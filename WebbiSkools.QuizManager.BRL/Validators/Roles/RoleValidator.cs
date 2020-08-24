using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.ViewModels.Roles;

namespace WebbiSkools.QuizManager.BRL.Validators.Roles
{
    public class RoleValidator : AbstractValidator<RoleCreate>
    {
        public RoleValidator()
        {
            RuleFor(q => q.Name).NotNull().NotEmpty().MinimumLength(5).MaximumLength(256);
        }
    }
}
