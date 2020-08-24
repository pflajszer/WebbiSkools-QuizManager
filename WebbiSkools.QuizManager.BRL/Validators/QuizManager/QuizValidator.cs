using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;

namespace WebbiSkools.QuizManager.BRL.Validators.QuizManager
{
    public class QuizValidator : AbstractValidator<QuizViewModel>
    {
		public QuizValidator()
		{
			RuleFor(q => q.Name).NotNull().NotEmpty().MinimumLength(5).MaximumLength(256);
		}
    }
}
