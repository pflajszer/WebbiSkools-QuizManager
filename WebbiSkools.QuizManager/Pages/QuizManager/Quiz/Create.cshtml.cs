using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;
using WebbiSkools.QuizManager.DAL.Entities.Identity;

namespace WebbiSkools.QuizManager.Pages.QuizManager.Quiz
{
    [Authorize(Roles = "Admin, Edit")]
    public class CreateModel : PageModel
    {
        private readonly IDataAccessMediator<QuizViewModel> _data;
        private readonly UserManager<QuizManagerUser> _userMgr;

        public CreateModel(IDataAccessMediator<QuizViewModel> data, UserManager<QuizManagerUser> userMgr)
        {
            _data = data;
            _userMgr = userMgr;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public QuizViewModel Quiz { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //var loggedInUserId = _userMgr.GetUserId(User);
            //Quiz.AuthorId = loggedInUserId;
            await _data.AddAsync(Quiz);

            return RedirectToPage("./Index");
        }
    }
}