using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;

namespace WebbiSkools.QuizManager.BRL.Validators.QuizManager
{
    public class QuestionValidator : AbstractValidator<QuestionViewModel>
    {
		public QuestionValidator()
		{
			RuleFor(q => q.Text).NotNull().NotEmpty().WithMessage("Question text cannot be empty!").MinimumLength(5).MaximumLength(512);
			RuleFor(q => q.Answers)
				.NotNull()
				.NotEmpty()
				.Must(a => a.Count >= 3)
				.WithMessage("A Question must have at least 3 answers!")
				.Must(x => x.Count <= 5).WithMessage("A Question can have maximum 5 answers!");

		}
	}
}
