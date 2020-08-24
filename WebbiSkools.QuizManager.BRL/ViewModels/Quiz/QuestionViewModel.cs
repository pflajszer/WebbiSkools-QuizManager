using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz.Base;

namespace WebbiSkools.QuizManager.BRL.ViewModels.Quiz
{
    public class QuestionViewModel : QuizElementBaseViewModel
    {
		public QuestionViewModel()
		{
            Answers ??= new List<AnswerViewModel>();
		}
        [DisplayName("Quiz Id")]
        public int? QuizId { get; set; }
        public QuizViewModel Quiz { get; set; }
        [DisplayName("Question Text")]
        public string Text { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
}
