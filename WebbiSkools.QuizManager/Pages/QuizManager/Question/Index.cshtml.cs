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

namespace WebbiSkools.QuizManager.Pages.QuizManager.Question
{
    [Authorize(Roles = "Admin, Edit")]
    public class IndexModel : PageModel
    {
        private readonly IDataAccessMediator<QuestionViewModel> _data;
        private readonly IQuizManagementProvider _svc;

        public IndexModel(IDataAccessMediator<QuestionViewModel> data, IQuizManagementProvider svc)
        {
            _data = data;
            _svc = svc;
        }

        public IList<QuestionViewModel> AssignedQuestions { get; set; }
        public IList<QuestionViewModel> NotAssignedQuestions { get; set; }
        public IList<QuestionViewModel> AllQuestions { get; set; }
        [BindProperty]
        public int? QuizId { get; set; }
        [BindProperty]
        public QuestionViewModel Question { get; set; }
        [BindProperty]
        public int? QuestionId { get; set; }

        public async Task OnGetAsync(int? quizId)
        {
            var allQuestions = await _data.GetAsync();
            if (quizId != null)
            {
                ViewData["QuizId"] = quizId;
                AssignedQuestions = allQuestions.Where(x => x.QuizId == quizId).ToList();
                NotAssignedQuestions = allQuestions.Where(x => x.QuizId is null).ToList();
            }
			else
			{
                AllQuestions = allQuestions;
            }
        }

        public async Task<IActionResult> OnPostAddToQuiz()
        {
            var allQuestions = await _data.GetAsync();
            var questionToAssign = allQuestions.FirstOrDefault(m => m.Id == QuestionId);
            questionToAssign.QuizId = QuizId;
            await _data.UpdateAsync(questionToAssign);
            return RedirectToPage("./Index", new { QuizId });
        }

        public async Task<IActionResult> OnPostAssignToCurrentQuiz(int questionId, int quizId)
		{
            await _svc.AssignQuestionToQuiz(quizId, questionId);
            return RedirectToPage("./Index", new { QuizId = quizId });
		}

        public async Task<IActionResult> OnPostUnassignQuestionFromCurrentQuiz(int questionId, int quizId)
        {
            await _svc.UnassignQuestionFromQuiz(quizId, questionId);
            return RedirectToPage("./Index", new { QuizId = quizId });
        }
    }
}