using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager.Base;

namespace WebbiSkools.QuizManager.DAL.Entities.QuizManager
{
    public class EAnswer : QuizElementBase
    {
		public int QuestionId { get; set; }
		public string Text { get; set; }
		public bool IsCorrect { get; set; }

		public virtual EQuestion Question { get; set; }
	}
}
