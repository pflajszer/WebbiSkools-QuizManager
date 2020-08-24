using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz.Base;

namespace WebbiSkools.QuizManager.BRL.ViewModels.Quiz
{
    public class QuizViewModel : QuizElementBaseViewModel
    {
		public QuizViewModel()
		{
			Questions ??= new List<QuestionViewModel>();
		}
		[DisplayName("Quiz Name")]
		public string Name { get; set; }
		public ICollection<QuestionViewModel> Questions { get; set; }
	}
}
