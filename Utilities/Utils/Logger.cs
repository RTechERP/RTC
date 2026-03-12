using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace BMS
{
    public class Logger
    {
        private readonly Component[] _components;
        private readonly string[] _actions;
        private readonly Func<dynamic, dynamic>[] _logDatas;
        private readonly Form _form;
        public dynamic oldLogData;
        /// <summary>
        /// Logger for... logging data?
        /// </summary>
        /// <param name="components">Controls that needs monitoring</param>
        /// <param name="actions">Strings that represent the action of the corresponding controls (eg: "Add", "Update")</param>
        /// <param name="logDatas">Functions that return the log data</param>
        /// <param name="form">Current form that's using logger</param>
        /// <param name="oldLogData">(Optional) Initial data of the form</param>
        public Logger(Component[] components, string[] actions, Func<dynamic, dynamic>[] logDatas,
            Form form, dynamic oldLogData = null)
        {
            _components = components;
            _actions = actions;
            _logDatas = logDatas;
            _form = form;
            this.oldLogData = oldLogData;
        }
        public void Start()
        {
            if (_logDatas.Length != _components.Length) throw new ArgumentException("Length does not match");
            for (int i = 0; i < _components.Length; i++)
            {
                var index = i;
                void handler(object s, EventArgs e)
                {
                    //Task.Run(() =>
                    //{
                    var nameProp = _components[index].GetType().GetProperty("Name");
                    string controlName = nameProp?.GetValue(_components[index]) as string ?? "(unnamed)";
                    var newLog = new ActivityLogsModel
                    {
                        LogTime = DateTime.Now,
                        UserID = Global.UserID,
                        EmployeeID = Global.EmployeeID,
                        Application = "RTC Winforms",
                        FormName = _form.Name,
                        ControlName = controlName,
                        Action = _actions[index],
                        Details = JsonConvert.SerializeObject(_logDatas[index](oldLogData))
                    };
                    SQLHelper<ActivityLogsModel>.Insert(newLog);
                    //});
                }

                if (_components[i] is GridControl grid)
                {
                    if (grid.MainView is GridView view)
                    {
                        view.CellValueChanged += (s, e) =>
                        {
                            handler(s, EventArgs.Empty);
                        };

                        view.ShownEditor += (s, e) =>
                        {
                            var activeEditor = view.ActiveEditor;
                            if (activeEditor != null)
                            {
                                //activeEditor.EditValueChanged -= handler;
                                activeEditor.EditValueChanged += handler;
                            }
                        };
                    }
                }
                else if (_components[i] is BaseEdit baseEdit)
                {
                    baseEdit.EditValueChanged += handler;
                }
                else if (_components[i] is SimpleButton button)
                {
                    button.Click += handler;
                }
                else if (_components[i] is ToolStripMenuItem menuitem)
                {
                    EventUtils.PrependEventHandler(menuitem, "EventClick", new EventHandler(handler));
                }
                else if (_components[i] is ToolStripButton tsbtn)
                {
                    EventUtils.PrependEventHandler(tsbtn, "EventClick", new EventHandler(handler));
                }
                else if (_components[i] is TextBox txtbox)
                {
                    txtbox.TextChanged += handler;
                }
                else if (_components[i] is SearchLookUpEdit cbo)
                {
                    EventUtils.PrependEventHandler(cbo, "EditValueChanged", new EventHandler(handler));
                }
            }
        }
    }

    public static class EventUtils
    {
        public static void PrependEventHandler(Component component, string eventName, EventHandler prependHandler)
        {
            if (component == null || prependHandler == null)
                throw new ArgumentNullException();

            object eventKey = null;
            EventHandlerList eventHandlerList = null;

            if (component is Control control)
            {
                eventKey = GetEventKey(control.GetType(), eventName);
                eventHandlerList = (EventHandlerList)typeof(Component)
                    .GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(control);
            }
            else if (component is ToolStripItem tsi)
            {
                eventKey = GetEventKey(tsi.GetType(), eventName);
                eventHandlerList = (EventHandlerList)typeof(Component)
                    .GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(tsi);
            }
            else
            {
                throw new NotSupportedException($"Component type '{component.GetType().Name}' not supported.");
            }

            if (eventKey == null || eventHandlerList == null)
                throw new InvalidOperationException("Failed to locate internal event information.");

            var existingHandler = eventHandlerList[eventKey];

            if (existingHandler != null)
            {
                foreach (Delegate d in existingHandler.GetInvocationList())
                    eventHandlerList.RemoveHandler(eventKey, d);
            }

            eventHandlerList.AddHandler(eventKey, prependHandler);

            if (existingHandler != null)
            {
                foreach (Delegate d in existingHandler.GetInvocationList())
                    eventHandlerList.AddHandler(eventKey, d);
            }
        }

        private static object GetEventKey(Type type, string fieldName)
        {
            while (type != null)
            {
                var field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
                if (field != null)
                    return field.GetValue(null);
                type = type.BaseType;
            }
            return null;
        }
    }
}