using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Classes
{
    public class DelegateEvents
    {
        public delegate void ChildFormAcivated(object sender);
        public delegate void ChildFormClosed(object sender);
    }
}
