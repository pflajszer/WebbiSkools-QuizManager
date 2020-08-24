using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager.Base;

namespace WebbiSkools.QuizManager.DAL.Entities.QuizManager
{
    public class EQuiz : QuizElementBase
    {
		public EQuiz()
		{
			Questions ??= new List<EQuestion>();
		}

		public string Name { get; set; }
		public ICollection<EQuestion> Questions { get; set; }
	}
}
