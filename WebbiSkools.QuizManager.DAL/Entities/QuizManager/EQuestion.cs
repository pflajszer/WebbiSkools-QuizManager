using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager.Base;

namespace WebbiSkools.QuizManager.DAL.Entities.QuizManager
{
    public class EQuestion : QuizElementBase
    {
		public EQuestion()
		{
			Answers ??= new List<EAnswer>();
		}
		public int? QuizId { get; set; }
		public EQuiz Quiz { get; set; }
		public string Text { get; set; }
		public virtual ICollection<EAnswer> Answers { get; set; }
	}
}
