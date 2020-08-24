using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbiSkools.QuizManager.BRL.ViewModels.Quiz.Base
{
    public class QuizElementBaseViewModel
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }
        [DisplayFormat(NullDisplayText = "Never modified")]
        [DisplayName("Last Modified")]
        public DateTime? LastModified { get; set; }
    }
}
