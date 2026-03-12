using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS
{
    public class ExamContentModel
    {
        public int ExamResultDetailID { get; set; }
        public int CourseQuestionID { get; set; }
        public string QuestionText { get; set; }
        public bool Status { get; set; } = false;//check xem câu hỏi đã được lưu hay chưa
        public List<ExamContentDTO> AnswerContent { get; set; }
        public string ImageName { get; set; }
    }
}
