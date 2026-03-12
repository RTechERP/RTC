using BMS.Business;
using BMS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public class EmailSender
    {
        private static string templateFileName;
        private static string tempFileName;

        public static void setEmail(string projectCode /*int ProjectID*/, ProjectPartListModel model, bool update)
        {
            EmployeeSendEmailModel email = new EmployeeSendEmailModel();

            email.Subject = $"BIẾN ĐỘNG PARTLIST - DỰ ÁN {projectCode}";

            //if (update)
            //    email.Subject = $"CẬP NHẬT THÔNG TIN TIẾN ĐỘ VẬT TƯ CHO SẢN PHẨM {model.ProductCode} THUỘC DỰ ÁN {ProjectCodeName(ProjectID)}";
            //    email.Subject = $"CẬP NHẬT THÔNG TIN TIẾN ĐỘ VẬT TƯ CHO SẢN PHẨM {model.ProductCode} THUỘC DỰ ÁN {ProjectCodeName(ProjectID)}";
            //else
            //    email.Subject = $"XÓA THÔNG TIN TIẾN ĐỘ VẬT TƯ SẢN PHẨM {model.ProductCode} THUỘC DỰ ÁN {ProjectCodeName(ProjectID)}";

            //email.Body = $"THÔNG TIN TIẾN ĐỘ VẬT TƯ ĐÃ ĐƯỢC CẬP NHẬT: STT: {model.STT} - Tên vật tư: {model.GroupMaterial} - Mã thiết bị: {model.ProductCode} - Thông số kỹ thuật: {model.Model}" +
            //    $"Người phụ trách {TextUtils.ToString(cbPersonManager.EditValue)}"+
            //    $"- Nhà sản xuất: {model.Manufacturer} - Đơn vị: {model.Unit} - Số lượng / 1 máy: {model.QtyMin} - Số lượng tổng: {model.QtyFull} - Giá: {model.Price} - Thành tiền: {model.Amount}" +
            //    $"- {model.VAT}  - LeadTime: {model.LeadTime} - Ngày về dự kiến: {model.ExpectedReturnDate} - Tình trạng: {model.Status} - {model.EmployeeID} - " +
            //    $"Nhà cung cấp: {model.NCC} - Ngày yêu cầu: {model.RequestDate}";

            setEmailBody(model, true);
            email.Body = File.ReadAllText(tempFileName);
            email.EmployeeID = ProjectPartListJoiner.UpdaterID;
            email.Receiver = ProjectPartListJoiner.ProjectManagerID;
            email.EmailTo = EmployeeEmail(email.Receiver);

            List<int> ccIDs = ProjectPartListJoiner.LeaderIDs.Concat(ProjectPartListJoiner.ChargerIDs).ToList();
            ccIDs.Insert(0, ProjectPartListJoiner.UserTechnicalID);
            ccIDs.Insert(0, ProjectPartListJoiner.SaleID);
            IEnumerable<int> distinctNumbers = ccIDs.Distinct();

            //email.EmailCC = $"{EmployeeEmail(ProjectPartListJoiner.SaleID)}; {EmployeeEmail(ProjectPartListJoiner.UserTechnicalID)}; " +
            //    $"{EmailCC(ProjectPartListJoiner.LeaderIDs)} {EmailCC(ProjectPartListJoiner.ChargerIDs)}";

            email.EmailCC = $"{EmailCC(distinctNumbers)}";
            //email.EmailCC = $"letheanh040499@gmail.com";
            email.DateSend = DateTime.Now;
            email.StatusSend = 1;

            EmployeeSendEmailBO.Instance.Insert(email);
        }

        private static string ProjectCodeName(int ProjectID)
        {
            ProjectModel project = (ProjectModel)ProjectBO.Instance.FindByPK(ProjectID);
            return $"{project.ProjectCode} - {project.ProjectName}";
        }

        private static string EmailCC(IEnumerable<int> ccIDs)
        {
            string result = "";
            foreach (int id in ccIDs)
            {
                ArrayList arr = EmployeeBO.Instance.FindByAttribute("UserID", id);
                if (arr.Count == 0)
                {
                    continue;
                }
                EmployeeModel employee = (EmployeeModel)arr[0];
                string mail = EmployeeEmail(employee.ID);
                if (!String.IsNullOrEmpty(mail))
                    result += mail + "; ";
            }
            return result;
        }


        private static string EmployeeEmail(int EmployeeID)
        {
            EmployeeModel employee = (EmployeeModel)EmployeeBO.Instance.FindByPK(EmployeeID);

            if (employee != null)
            {
                return (String.IsNullOrEmpty(employee.EmailCongTy) ? employee.EmailCaNhan : employee.EmailCongTy) /*+ "; "*/;
            }
            return "";
        }

        private static void ReplaceFileText(string filePath, string oldText, string newText, bool once)
        {
            string fileContent = File.ReadAllText(filePath);

            if (once)
            {
                int index = fileContent.IndexOf(oldText);
                if (index != -1)
                {
                    fileContent = fileContent.Substring(0, index) + newText + fileContent.Substring(index + oldText.Length);
                    File.WriteAllText(filePath, fileContent);
                    return;
                }
            }

            fileContent = fileContent.Replace(oldText, newText);
            File.WriteAllText(filePath, fileContent);
        }

        private static void setEmailBody(ProjectPartListModel model, bool update)
        {
            templateFileName = Path.Combine(Application.StartupPath, "mail_template.txt");
            tempFileName = Path.Combine(Application.StartupPath, "mail_temporary.txt");
            File.Copy(templateFileName, tempFileName, true);

            if (update)
            {
                ReplaceFileText(tempFileName, "@Heading", $"CẬP NHẬT THÔNG TIN PARTLIST PRODUCT {model.ProductCode} THUỘC DỰ ÁN {model.ProjectID}", true);
            }

            string duplicatedText = String.Concat(Enumerable.Repeat("<li>@FieldName: @FieldValue</li>", 19));
            ReplaceFileText(tempFileName, "<li>@FieldName: @FieldValue</li>", duplicatedText, true);
            var props = model.GetType().GetProperties();
            foreach (var item in props)
            {
                string t = item.Name;
                string v = TextUtils.ToString(item.GetValue(model));
                ReplaceFileText(tempFileName, "@FieldName", t, true);
                ReplaceFileText(tempFileName, "@FieldValue", v, true);
            }
        }

        public static void SendEmail(string subject, string body /*html*/, int receiverID /*employeeID*/, string cc /*list CC*/)
        {
            Microsoft.Office.Interop.Outlook.MailItem oMsg;
            Microsoft.Office.Interop.Outlook.Application oApp;
            oApp = new Microsoft.Office.Interop.Outlook.Application();
            try
            {
                if (receiverID <= 0) return;
                EmployeeModel receiver = SQLHelper<EmployeeModel>.FindByID(receiverID);
                if (receiver == null) return;
                string receiverEmail = receiver.EmailCongTy;
                oMsg = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                oMsg.Subject = subject;
                oMsg.To = TextUtils.ToString(receiver.EmailCongTy).Trim();
                List<string> listCC = new List<string>();
                if (cc.Length > 0)
                {
                    string[] arrCC = cc.Split(';');
                    for (int j = 0; j < arrCC.Length; j++)
                    {
                        string mail = arrCC[j];
                        if (!mail.Contains("@")) continue;
                        listCC.Add(arrCC[j]);
                    }
                }
                oMsg.CC = string.Join(";", listCC);
                oMsg.HTMLBody = body;
                oMsg.Send();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                oMsg = null;
            }
        }
    }
}
