using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions;
using WebbiSkools.QuizManager.BRL.Services.Abstractions;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;

namespace WebbiSkools.QuizManager.Pages.QuizManager.Question
{
    [Authorize(Roles = "Admin, Edit")]
    public class EditModel : PageModel
    {
        private readonly IDataAccessMediator<QuestionViewModel> _questionData;
        private readonly IDataAccessMediator<AnswerViewModel> _answerData;
        private readonly IQuizManagementProvider _svc;

        public EditModel(IDataAccessMediator<QuestionViewModel> questionData,
                         IDataAccessMediator<AnswerViewModel> answerData,
                         IQuizManagementProvider svc)
        {
            _svc = svc;
            _questionData = questionData;
            _answerData = answerData;
        }

        [BindProperty]
        public QuestionViewModel Question { get; set; }
        [BindProperty]
        public IList<AnswerViewModel> NewAnswers { get; set; }

        [FromQuery]
		public string QuizId { get; set; }

        [FromQuery]
        public int? QuestionId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

            Question = await _questionData.GetByIdAsync((int)id);

			if (Question == null)
			{
				return NotFound();
			}
            var selectEmptyIf = Question.QuizId == null && Question.Quiz == null;

            var quizesDropdownValues = await _svc.GenerateQuizesDropdown(selectEmptyIf);
			ViewData["QuizId"] = quizesDropdownValues;

			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Question.QuizId == -1)
            {
                Question.Quiz = null;
                Question.QuizId = null;
            }


            foreach (var ans in NewAnswers.ToList())
            {
                if (string.IsNullOrEmpty(ans.Text))
                {
                    NewAnswers.Remove(ans);
                }
				else
				{
                    await _answerData.AddAsync(ans);
				}
            }
            foreach (var ans in Question.Answers.ToList())
            {
				if (string.IsNullOrEmpty(ans.Text))
				{
					Question.Answers.Remove(ans);
					if (ans.QuestionId != 0)
					{
						await _answerData.DeleteHardAsync(ans.Id);
					}
				}
				else
				{
                    await _answerData.UpdateAsync(ans);
				}
            }

            try
            {
                await _questionData.UpdateAsync(Question);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await QuestionExists(Question.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            if (QuizId != null)
            {
                return RedirectToPage("./Index", new { QuizId });
            }
            else
			{
                return RedirectToPage("./Index");
			}
        }

        public async Task<IActionResult> OnPostDeleteQuestion()
        {
            if (QuestionId != null)
            {
                await _questionData.DeleteHardAsync((int)QuestionId);
            }
            return RedirectToPage("./Index");
        }

        private async ValueTask<bool> QuestionExists(int id)
        {
            return await _questionData.IsAnyWithIdAsync(id);
        }
    }
}