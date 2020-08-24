using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;

namespace WebbiSkools.QuizManager.Pages.QuizManager.Question
{
    [Authorize(Roles = "Admin, Edit")]
    public class DetailsModel : PageModel
    {
        private readonly IDataAccessMediator<QuestionViewModel> _data;
		public DetailsModel(IDataAccessMediator<QuestionViewModel> data)
		{
            _data = data;
		}

		public QuestionViewModel Question { get; set; }
        [FromQuery]
		public int Id { get; set; }

		public async Task OnGet()
        {
            if (Id != 0)
            {
                Question = await _data.GetByIdAsync(Id);
            }
        }
    }
}