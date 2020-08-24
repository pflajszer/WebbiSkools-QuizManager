using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbiSkools.QuizManager.DAL.Entities.QuizManager.Base
{
    public abstract class QuizElementBase
    {
		public int Id { get; set; }
		public bool Deleted { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? LastModified { get; set; }

	}
}
