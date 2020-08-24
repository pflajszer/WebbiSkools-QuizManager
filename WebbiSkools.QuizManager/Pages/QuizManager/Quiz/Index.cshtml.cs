using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions;
using WebbiSkools.QuizManager.BRL.Services.Abstractions;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;

namespace WebbiSkools.QuizManager.Pages.QuizManager.Quiz
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IDataAccessMediator<QuizViewModel> _data;

        public IndexModel(IDataAccessMediator<QuizViewModel> data)
        {
            _data = data;
        }

        public IList<QuizViewModel> Quiz { get; set; }

        public async Task OnGetAsync()
        {
            Quiz = await _data.GetAsync();
        }
    }
}