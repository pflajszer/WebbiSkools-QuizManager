using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.DAL.Entities.Identity;

namespace WebbiSkools.QuizManager.BRL.ViewModels.Roles
{
    public class RoleEdit
    {
		public IdentityRole Role { get; set; }
		public IEnumerable<QuizManagerUser> Members { get; set; }
        public IEnumerable<QuizManagerUser> NonMembers { get; set; }
    }
}
