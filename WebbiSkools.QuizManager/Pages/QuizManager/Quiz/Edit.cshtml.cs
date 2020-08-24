using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;

namespace WebbiSkools.QuizManager.Pages.QuizManager.Quiz
{
    [Authorize(Roles="Admin, Edit")]
    public class EditModel : PageModel
    {
        private readonly IDataAccessMediator<QuizViewModel> _data;

        public EditModel(IDataAccessMediator<QuizViewModel> data)
        {
            _data = data;
        }

        [BindProperty]
        public QuizViewModel Quiz { get; set; }
        [FromQuery]
        [FromBody]
		public int? QuizId { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
            if (QuizId == null)
            {
                return NotFound();
            }

            Quiz = await _data.GetByIdAsync((int)QuizId);

            if (Quiz == null)
            {
                return NotFound();
            }
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

            try
            {
                await _data.UpdateAsync(Quiz);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EQuizExists(Quiz.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { QuizId });

        }

        public async Task<IActionResult> OnPostDeleteQuiz()
		{
            if (QuizId != null)
			{
                await _data.DeleteHardAsync((int)QuizId);
			}
            return RedirectToPage("./Index");
		}

        private async ValueTask<bool> EQuizExists(int id)
        {
            return await _data.IsAnyWithIdAsync(id);
        }
    }
}