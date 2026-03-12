using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmExamMultipleChoice : _Forms
    {
        private string _server = "http://192.168.1.2:8083";
        //private string _server = "http://localhost:8390";

        private Thread _threadLive;
        private int _examID = 0;
        private int _totalSecond = 0;
        private int _currentQuestionIndex = -1;
        private int _oldQuestionIndex = -1;
        private bool _isFinish = false;
        private List<ExamContentModel> _listExamContent = new List<ExamContentModel>();
        private ExamResultModel _examResult = null;
        private List<Button> _lstButton = new List<Button>();

        public frmExamMultipleChoice()
        {
            InitializeComponent();
        }

        private void frmExamMultipleChoice_Load(object sender, EventArgs e)
        {
            lblUserInfo.Text = $"Xin chào, {Global.AppCodeName} - {Global.AppFullName}";
            lblExamResult.Text = "";
            LoadExamType();

            if (Global.IsOnline) _server = "http://113.190.234.64:8083";

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.EmployeeID <= 0)
                {
                    MessageBox.Show("Bạn chưa đăng nhập hệ thống!");
                    return;
                }

                _oldQuestionIndex = 0;

                HttpClient client = new HttpClient();
                string url = $"{_server}/api/ExamNew/tests";
                var postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "YearValue", TextUtils.ToString( txtYear.Value) },
                    { "Season", TextUtils.ToString( txtSeason.Value )},
                    { "TestType", TextUtils.ToString( cboExamType.SelectedValue) },
                    { "EmployeeID", TextUtils.ToString( Global.EmployeeID) },
                    { "LoginName",Global.LoginName }
                });
                postData.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var req = client.PostAsync(url, postData);
                var r = req.Result;
                string resultJsonString = r.Content.ReadAsStringAsync().Result;
                dynamic res = JObject.Parse(resultJsonString);

                if (res.status == 0)
                {
                    MessageBox.Show(TextUtils.ToString(res.message), "Thông báo");
                    return;
                }
                GetQuestions();
                MessageBox.Show(Convert.ToString(res.message), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _examID = res.data.examID;
                int totalAmount = res.data.total;
                decimal duration = res.data.duration;
                _totalSecond = (int)(duration * 60);


                TimeSpan time = TimeSpan.FromSeconds(_totalSecond);

                lblExamDetail.Text = $@"Số câu: {totalAmount} - Thời lượng: {time.ToString(@"hh\:mm\:ss")} phút";
                //lblStatus.Text = $"Số câu đã trả lời\n0/{totalAmount}";

                btnFinish.Enabled = btnBackQuestion.Enabled = btnNextQuestion.Enabled = btnSave.Enabled = true;
                txtYear.Enabled = txtSeason.Enabled = cboExamType.Enabled = btnStart.Enabled = false;

                LoadAllQuestion();

                //Load luôn câu hỏi đầu tiên lên
                _currentQuestionIndex = 0;
                LoadQuestion(_currentQuestionIndex);

                //Điếm ngược thời gian
                _threadLive = new Thread(new ThreadStart(RunCount));
                _threadLive.IsBackground = true;
                _threadLive.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void LoadExamType()
        {
            DateTime now = DateTime.Now;
            int currentYear = now.Year;
            int currentMonth = now.Month;
            int currentQuarter = (currentMonth - 1) / 3 + 1;

            txtYear.Value = currentYear;
            txtSeason.Value = currentQuarter;

            List<object> examType = new List<object>
            {
                new {Value = 1, Text = "Vision"},
                new {Value = 2, Text = "Điện"},
                new {Value = 3, Text = "Phần mềm"},
                new {Value = 4, Text = "Nội quy"},
                new {Value = 5, Text = "AGV"},
                new {Value = 6, Text = "Tester"},
            };
            cboExamType.DataSource = examType;
            cboExamType.ValueMember = "Value";
            cboExamType.DisplayMember = "Text";

            lblExamDetail.Text = "";
        }

        private void LoadAllQuestion() //display the buttons in bottom right corner
        {
            _lstButton.Clear();
            int count = _listExamContent.Count;
            lblStatus.Text = $"Số câu đã trả lời\n0/{count}";
            for (int i = 0; i < count; i++)
            {
                Button button = new Button();
                button.Tag = i;
                button.Name = "b" + i;
                button.Text = (i + 1).ToString();
                button.Width = 40;
                button.Height = 30;
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = Color.LightGray;
                button.Click += new EventHandler(this.button_Click);
                flpListQuestion.Controls.Add(button);
                _lstButton.Add(button);
            }
        }

        private void GetQuestions()
        {
            int yearValue = TextUtils.ToInt(txtYear.Value);
            int season = TextUtils.ToInt(txtSeason.Value);
            int testType = TextUtils.ToInt(cboExamType.SelectedValue);
            int employeeID = Global.EmployeeID;
            HttpClient client = new HttpClient();
            string url = $"{_server}/api/ExamNew/tests?YearValue={yearValue}&Season={season}&TestType={testType}&EmployeeID={employeeID}";
            var req = client.GetAsync(url);
            var r = req.Result;
            string resultJsonString = r.Content.ReadAsStringAsync().Result;
            dynamic res = JObject.Parse(resultJsonString);
            if (res.status == 0) throw new Exception(Convert.ToString(res.message));
            List<ExamContentDTO> data = res.data.ToObject<List<ExamContentDTO>>();
            _listExamContent = data.GroupBy(
            x => new { x.ExamResultDetailID, x.CourseQuestionID, x.QuestionText, x.ImageName })
                .Select(g => new ExamContentModel
                {
                    ExamResultDetailID = g.Key.ExamResultDetailID,
                    CourseQuestionID = g.Key.CourseQuestionID,
                    QuestionText = g.Key.QuestionText,
                    Status = false,
                    AnswerContent = g.ToList(),
                    ImageName = g.Key.ImageName,
                })
                .ToList();
        }

        private void LoadQuestion(int questionIndex) // load a particular question
        {
            //// Mã HTML bạn muốn hiển thị
            //string htmlContent = @"
            //    <h2 style='white-space: pre-wrap; width: 100%; font-family: Arial, Helvetica, sans-serif'>
            //        Cho sơ đồ chân đầu ra của 1 cảm biến như hình dưới. <br />Và cho Nguồn 24V và 0V. <br />
            //        Hãy đấu nối tín hiệu của cảm biến vào bộ đếm để bộ đếm có thể nhận được tín hiệu của cảm biến.
            //    </h2>
            //    <hr />
            //    <div style='display: grid; grid-template-columns: auto auto;'>
            //        <table style='border-collapse: collapse; font-family: Arial, Helvetica, sans-serif'>
            //            <tr>
            //                <td style='display: flex; align-items: center; flex-wrap: nowrap'>
            //                    <input type='checkbox' value='434' name='434' style='width: 30px; height: 30px' />
            //                    <label for='434' style='font-size: 20px; margin-left: 10px'>
            //                        A.+V của sensor và chân 2 của bộ đếm nối với 24V, Chân COM của cảm biến nối với 0V, 
            //                        Chân OUT nối với chân 3 của bộ đếm.
            //                    </label>
            //                </td>
            //            </tr>
            //            <tr>
            //                <td style='display: flex; align-items: center; flex-wrap: nowrap'>
            //                    <input type='checkbox' value='435' name='435' style='width: 30px; height: 30px' />
            //                    <label for='435' style='font-size: 20px; margin-left: 10px'>
            //                        B.+V của sensor nối với 24V, Chân COM của cảm biến nối với 0V, Chân OUT nối với chân 3 
            //                        của bộ đếm, Chận 2 của bộ đếm nối 0V.
            //                    </label>
            //                </td>
            //            </tr>
            //            <tr>
            //                <td style='display: flex; align-items: center; flex-wrap: nowrap'>
            //                    <input type='checkbox' value='437' name='437' style='width: 30px; height: 30px' />
            //                    <label for='437' style='font-size: 20px; margin-left: 10px'>
            //                        C.+V của sensor nối với 24V, Chân COM của cảm biến nối với 0V, Chân OUT nối với chân 3 
            //                        của bộ đếm, Chận 1 của bộ đếm nối 0V.
            //                    </label>
            //                </td>
            //            </tr>
            //            <tr>
            //                <td style='display: flex; align-items: center; flex-wrap: nowrap'>
            //                    <input type='checkbox' value='436' name='436' style='width: 30px; height: 30px' />
            //                    <label for='436' style='font-size: 20px; margin-left: 10px'>
            //                        D.+V của sensor và chân 1 của bộ đếm nối với 24V, Chân COM của cảm biến nối với 0V, 
            //                        Chân OUT nối với chân 3 của bộ đếm.
            //                    </label>
            //                </td>
            //            </tr>
            //        </table>
            //        <img src='http://192.168.1.2:8083/api/Upload/Images/Courses/34_110823142119.PNG' alt='' style='border: 2px solid black' />
            //    </div>
            //";

            //// Tải nội dung HTML vào WebBrowser
            //wbQuestion.DocumentText = htmlContent;


            //return;

            if (questionIndex > _listExamContent.Count - 1 || questionIndex < 0) return;

            _lstButton[questionIndex].BackColor = Color.Yellow;
            if (_oldQuestionIndex >= 0 && _currentQuestionIndex != _oldQuestionIndex)
                _lstButton[_oldQuestionIndex].BackColor =
                    !_listExamContent[_oldQuestionIndex].Status
                    ? Color.LightGray : Color.Lime;

            ExamContentModel question = _listExamContent[questionIndex];
            if (question.Status)
            {
                // do something when load saved question? doesnt seem to be needed... yet
            }

            string questionText = question.QuestionText.Trim().Replace("\n", "<br>");
            string contentQuestion = $"<h2 style=\"white-space: pre-wrap; width: 100%; font-family: Arial, Helvetica, sans-serif;\">{questionText}</h2><hr>";
            string htmlText = $"{contentQuestion}";
            htmlText += "<div style=\"display: flex; justify-items: center; justify-content: space-between;\">";
            htmlText += "<table style='border-collapse: collapse; font-family: Arial, Helvetica, sans-serif;'>";

            string urlImage = $"{_server}/api/Upload/Images/Courses/{question.ImageName}";
            string htmlImage = string.IsNullOrEmpty(question.ImageName) ? "</div>" : $"<img src=\"{urlImage}\" alt=\"\" style=\"border: 2px solid black; \"></div>";

            string htmlAnswer = "";
            foreach (var answer in question.AnswerContent)
            {
                string code = answer.STT == 1 ? "A." :
                              answer.STT == 2 ? "B." :
                              answer.STT == 3 ? "C." :
                              answer.STT == 4 ? "D." : "";
                //htmlText += $@"<tr>
                //                    <td style='text-align: center;vertical-align: baseline;'>
                //                        <input 
                //                            type='checkbox' 
                //                            style='
                //                                width: 30px;
                //                                height: 30px;
                //                                cursor: pointer;
                //                                border: 1px solid white;
                //                                '
                //                            value='{answer.CourseAnswerID}'
                //                            {(answer.IsPicked ? "checked='true'" : "")} />
                //                    </td>
                //                    <td 
                //                        style='
                //                            vertical-align: baseline;
                //                            font-size: 20px;
                //                            padding-top: 4px;
                //                            padding-bottom: 15px'>
                //                        {code} {answer.AnswerText}
                //                    </td>
                //               </tr>";

                string isChecked = answer.IsPicked ? "checked" : "";
                htmlAnswer += $"<tr>" +
                                $"<td style=\"padding: 10px 5px;\">" +
                                    $"<input type=\"checkbox\" value=\"{answer.CourseAnswerID}\" name=\"{answer.CourseAnswerID}\" {isChecked} style=\"width: 30px; height: 30px;\"/>" +
                                    $"</td><td style=\"padding: 10px 0;\">" +
                                    $"<p style=\"font-size: 20px; margin-left: 10px;\"><b>{code}</b> {answer.AnswerText.Trim()}</p></td>" +
                                $"</td></tr>";
            }
            htmlText += htmlAnswer;
            htmlText += "</table>";
            htmlText += htmlImage;
            htmlText += @"<script>
                            function GetPickedAnswers() {
                                var checkboxes = document.getElementsByTagName('input');
                                    var values = [];
                                    for (var i = 0; i < checkboxes.length; i++)
                                    {
                                        if (checkboxes[i].type === 'checkbox' && checkboxes[i].checked) {
                                        values.push(checkboxes[i].value);
                                    }
                                }
                                return values.join(',');
                            }
                            function PickAnswer(answer){
                                var checkboxes = document.getElementsByTagName('input');
                                checkboxes[answer-1].checked = true;
                            }
                            </script>";

            wbQuestion.DocumentText = htmlText;
            wbQuestion.Document.ExecCommand("SelectAll", false, null);
            wbQuestion.Document.ExecCommand("Copy", false, null);
            if (wbQuestion.Document.Body != null) wbQuestion.Document.Body.KeyDown += captureKeyEventFromWebBrowser;

        }

        void NextQuestion()
        {
            if (_currentQuestionIndex == _listExamContent.Count - 1) return;
            SaveAnswers();
            _oldQuestionIndex = _currentQuestionIndex;
            _currentQuestionIndex++;
            LoadQuestion(_currentQuestionIndex);
        }

        private List<int> GetPickedAnswers()
        {
            var checkedValues = new List<int>();
            string result = wbQuestion.Document.InvokeScript("GetPickedAnswers") as string;

            if (!string.IsNullOrWhiteSpace(result))
            {
                foreach (var value in result.Split(','))
                {
                    if (int.TryParse(value, out int intValue))
                    {
                        checkedValues.Add(intValue);
                    }
                }
            }

            return checkedValues;
        }

        private void SaveAnswers()
        {
            var pickedAnswer = GetPickedAnswers();
            var currentQuestion = _listExamContent[_currentQuestionIndex];
            foreach (var answer in currentQuestion.AnswerContent)
            {
                if (pickedAnswer.Contains(answer.CourseAnswerID)) answer.IsPicked = true;
                else answer.IsPicked = false;
            }
        }

        private void RunCount()
        {
            //_totalSecond = 5000;
            while (true)
            {
                Thread.Sleep(1000);
                if (_totalSecond == 0 || _isFinish)
                {
                    Invoke(new Action(() =>
                    {
                        FinishExam();
                    }));

                    break;
                }
                _totalSecond--;
                try
                {
                    Invoke(new Action(() =>
                    {
                        TimeSpan time = TimeSpan.FromSeconds(_totalSecond);
                        lblTime.Text = time.ToString(@"hh\:mm\:ss");
                    }));
                }
                catch
                {

                }
            }
        }

        private void FinishExam()
        {
            btnSave.Enabled = btnBackQuestion.Enabled = btnNextQuestion.Enabled = btnFinish.Enabled = false;
            HttpClient client = new HttpClient();
            string url = $"{_server}/api/ExamNew/result";
            var postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "Id", _examID.ToString() },
                    { "EmployeeID",Global.EmployeeID.ToString() },
                    { "LoginName",Global.LoginName}
                });
            postData.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var req = client.PostAsync(url, postData);
            var r = req.Result;
            string resultJsonString = r.Content.ReadAsStringAsync().Result;
            dynamic res = JObject.Parse(resultJsonString);
            if (res.status == 0)
            {
                MessageBox.Show(Convert.ToString(res.message));
                return;
            }
            _examResult = GetExamResult() ?? MessageBox.Show(Convert.ToString(res.message)); ;
            //btnExamResult.Visible = true;
            btnExit.Visible = true;
            btnExamResult_Click(null, null);
        }

        private ExamResultModel GetExamResult()
        {
            HttpClient client = new HttpClient();
            string url = $"{_server}/api/ExamNew/result?Id={_examID}";
            var req = client.GetAsync(url);
            var r = req.Result;
            string resultJsonString = r.Content.ReadAsStringAsync().Result;
            dynamic res = JObject.Parse(resultJsonString);
            if (res.status == 0) return null;
            return res.data.ToObject<ExamResultModel>(); ;
        }


        private void button_Click(object sender, EventArgs e) // when pick a particular question
        {
            Button btn = (Button)sender;
            //if (_currentQuestionIndex == (int)btn.Tag) return;
            var examResult = wbQuestion.Document.GetElementById("result");
            if (examResult == null) SaveAnswers();
            _oldQuestionIndex = _currentQuestionIndex;
            _currentQuestionIndex = (int)btn.Tag;
            LoadQuestion(_currentQuestionIndex);
        }

        private void captureKeyEventFromWebBrowser(object sender, HtmlElementEventArgs e)
        {
            var keyEventArgs = new KeyEventArgs((Keys)e.KeyPressedCode);
            frmExamMultipleChoice_KeyDown(this, keyEventArgs);
            bool isCtrlPressed = (Control.ModifierKeys & Keys.Control) == Keys.Control;
            if (isCtrlPressed && e.KeyPressedCode == (int)Keys.S)
            {
                var saveEventArgs = new KeyEventArgs(Keys.Control | Keys.S);
                frmExamMultipleChoice_KeyDown(this, saveEventArgs);
            }
        }

        private void frmExamMultipleChoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (wbQuestion.Document == null) return;
            if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
            {
                wbQuestion.Document.InvokeScript("PickAnswer", new object[] { 1 });
            }

            if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
            {
                wbQuestion.Document.InvokeScript("PickAnswer", new object[] { 2 });
            }

            if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
            {
                wbQuestion.Document.InvokeScript("PickAnswer", new object[] { 3 });
            }

            if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)
            {
                wbQuestion.Document.InvokeScript("PickAnswer", new object[] { 4 });
            }

            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave_Click(null, null);
            }
            if (e.KeyCode == Keys.Right)
            {
                btnNextQuestion_Click(null, null);
            }
            if (e.KeyCode == Keys.Left)
            {
                btnBackQuestion_Click(null, null);
            }
        }

        private void wbQuestion_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            wbQuestion.Document.Body.KeyDown += captureKeyEventFromWebBrowser;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAnswers();
                var pickedAnswer = GetPickedAnswers();
                if (pickedAnswer.Count == 0)
                {
                    //btnNextQuestion_Click(null, null);
                    NextQuestion();
                    return;
                }
                var currentQuestion = _listExamContent[_currentQuestionIndex];
                currentQuestion.Status = true;

                HttpClient client = new HttpClient();
                string url = $"{_server}/api/ExamNew/answers";
                var data = currentQuestion.AnswerContent;
                string jsonData = JsonConvert.SerializeObject(data);
                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var req = client.PostAsync(url, stringContent);
                var r = req.Result;
                string resultJsonString = r.Content.ReadAsStringAsync().Result;
                dynamic res = JObject.Parse(resultJsonString);
                if (res.status == 0) throw new Exception(Convert.ToString(res.message));
                var amountAnswered = _listExamContent.Count(q => q.Status);
                var total = _listExamContent.Count();
                lblStatus.Text = $"Số câu đã trả lời\n{amountAnswered}/{total}";
                //btnNextQuestion_Click(null, null);
                NextQuestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra.\n {ex.Message}");
            }
        }

        private void btnBackQuestion_Click(object sender, EventArgs e)
        {
            if (_currentQuestionIndex == 0) return;
            SaveAnswers();
            _oldQuestionIndex = _currentQuestionIndex;
            _currentQuestionIndex--;
            LoadQuestion(_currentQuestionIndex);
        }

        private void btnNextQuestion_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAnswers();
                var pickedAnswer = GetPickedAnswers();
                if (pickedAnswer.Count == 0)
                {
                    //btnNextQuestion_Click(null, null);
                    NextQuestion();
                    return;
                }
                var currentQuestion = _listExamContent[_currentQuestionIndex];
                currentQuestion.Status = true;

                HttpClient client = new HttpClient();
                string url = $"{_server}/api/ExamNew/answers";
                var data = currentQuestion.AnswerContent;
                string jsonData = JsonConvert.SerializeObject(data);
                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var req = client.PostAsync(url, stringContent);
                var r = req.Result;
                string resultJsonString = r.Content.ReadAsStringAsync().Result;
                dynamic res = JObject.Parse(resultJsonString);
                if (res.status == 0) throw new Exception(Convert.ToString(res.message));
                var amountAnswered = _listExamContent.Count(q => q.Status);
                var total = _listExamContent.Count();
                lblStatus.Text = $"Số câu đã trả lời\n{amountAnswered}/{total}";
                //btnNextQuestion_Click(null, null);
                NextQuestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra.\n {ex.Message}");
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (_listExamContent.Count(q => q.Status == false) > 0)
            {
                // do something if there is an unsaved answer
            }
            if (MessageBox.Show("Bạn có chắc muốn nộp bài thi", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            _isFinish = true;

            //btnExamResult_Click(null, null);
        }

        private void btnExamResult_Click(object sender, EventArgs e)
        {
            //wbQuestion.DocumentText =
            //   $@"<div id='result' style='font-size:25px'>Kết quả bài thi:<br>
            //    - Tổng số đáp án đã chọn: {_examResult.TotalChoosen}<br>
            //    - Tổng số đáp án đúng: {_examResult.TotalCorrect}<br>
            //    - Tổng số đáp án sai: {_examResult.TotalInCorrect}<br>
            //    - Tổng điểm: {_examResult.TotalMarks}/{_listExamContent.Count()}</div>";

            _examResult = _examResult ?? new ExamResultModel();
            lblExamResult.Text = "KẾT QUẢ THI\n" +
                        $"- Số câu đã chọn: {_examResult.TotalChoosen}\n" +
                        $"- Số câu đúng: {_examResult.TotalCorrect}\n" +
                        $"- Số câu sai: {_examResult.TotalInCorrect}\n" +
                        $"TỔNG ĐIỂM: {_examResult.TotalMarks}/{_listExamContent.Count()}";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            _isFinish = false;
            //_login = new Login();
            _examID = 0;
            //btnLogin.Enabled = txtUser.Enabled = txtPass.Enabled = chkOnline.Enabled = true;
            //lblUserInfo.Text = "Bạn chưa đăng nhập";
            lblStatus.Text = "Bạn đã trả lời\n0/0";
            _totalSecond = 0;
            lblTime.Text = "00:00:00";
            txtYear.Enabled = txtSeason.Enabled = cboExamType.Enabled = btnStart.Enabled = true;
            lblExamDetail.Text = "";
            _listExamContent.Clear();
            _lstButton.Clear();
            flpListQuestion.Controls.Clear();
            wbQuestion.DocumentText = "";
            btnExamResult.Visible = btnExit.Visible = false;

            lblExamResult.Text = "";
        }

        private void frmExamMultipleChoice_MinimumSizeChanged(object sender, EventArgs e)
        {
            MessageBox.Show("out");
        }
    }
}
