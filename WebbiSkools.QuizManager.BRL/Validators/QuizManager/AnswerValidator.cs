using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;

namespace WebbiSkools.QuizManager.BRL.Validators.QuizManager
{
    public class AnswerValidator : AbstractValidator<AnswerViewModel>
    {
		public AnswerValidator()
		{
			RuleFor(a => a.Text).NotNull().NotEmpty().WithMessage("Answer Text cannot be empty").MinimumLength(5).MaximumLength(256);
		}
    }
}
