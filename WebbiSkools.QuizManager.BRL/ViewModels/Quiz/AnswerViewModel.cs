using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz.Base;

namespace WebbiSkools.QuizManager.BRL.ViewModels.Quiz
{
    public class AnswerViewModel : QuizElementBaseViewModel
    {
        [DisplayName("Question Id")]
        public int QuestionId { get; set; }
        [DisplayName("Answer Text")]
        public string Text { get; set; }
        [DisplayName("Correct?")]
        public bool IsCorrect { get; set; }
    }
}
