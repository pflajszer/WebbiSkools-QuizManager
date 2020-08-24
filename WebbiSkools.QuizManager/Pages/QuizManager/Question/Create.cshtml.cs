using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions;
using WebbiSkools.QuizManager.BRL.Services.Abstractions;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;

namespace WebbiSkools.QuizManager.Pages.QuizManager.Question
{
	[Authorize(Roles = "Admin, Edit")]
	public class CreateModel : PageModel
	{
		private readonly IDataAccessMediator<QuestionViewModel> _questionData;
		private readonly IDataAccessMediator<AnswerViewModel> _answerData;
		private readonly IQuizManagementProvider _svc;

		public CreateModel(IDataAccessMediator<QuestionViewModel> questionData, IDataAccessMediator<AnswerViewModel> answerData, IQuizManagementProvider svc)
		{
			_questionData = questionData;
			_answerData = answerData;
			_svc = svc;

		}

		[FromQuery]
		public string QuizId { get; set; }

		public async Task<IActionResult> OnGet()
		{
			SelectList quizesDropdownValues;
			if (QuizId != null)
			{
				quizesDropdownValues = await _svc.GenerateQuizesDropdown(specificIdToSelect: QuizId);

			}
			else
			{
				quizesDropdownValues = await _svc.GenerateQuizesDropdown(selectEmpty: true);

			}
			ViewData["QuizId"] = quizesDropdownValues;
			return Page();
		}

		[BindProperty]
		public QuestionViewModel Question { get; set; }

		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				var quizesDropdownValues = await _svc.GenerateQuizesDropdown(selectEmpty: true);
				ViewData["QuizId"] = quizesDropdownValues;
				return Page();
			}

			foreach(var ans in Question.Answers.ToList())
			{
				if (string.IsNullOrEmpty(ans.Text))
				{
					Question.Answers.Remove(ans);
				}
			}
			if (Question.QuizId == -1)
			{
				Question.Quiz = null;
				Question.QuizId = null;
			}
			await _questionData.AddAsync(Question);

			if (QuizId != null)
			{
				return RedirectToPage("./Index", new { QuizId });
			}
			else
			{
				return RedirectToPage("./Index");
			}
		}
	}
}