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
    public class DetailsModel : PageModel
    {
        private readonly IDataAccessMediator<QuizViewModel> _quizData;
        private readonly IDataAccessMediator<QuestionViewModel> _questionData;
        private readonly IQuizManagementProvider _svc;

        public DetailsModel(IDataAccessMediator<QuizViewModel> quizData,
                            IDataAccessMediator<QuestionViewModel> questionData,
                            IQuizManagementProvider svc)
        {
            _quizData = quizData;
            _questionData = questionData;
            _svc = svc;
        }

        public QuizViewModel Quiz { get; set; }
        [FromQuery]
        [FromBody]
		public int QuestionId { get; set; }
        [FromQuery]
        [FromBody]
        public int QuizId { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
            if (QuizId == 0)
            {
                return NotFound();
            }

            Quiz = await _quizData.GetByIdAsync(QuizId);

            if (Quiz == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUnassignQuestionFromCurrentQuiz(int questionId, int quizId)
        {
            await _svc.UnassignQuestionFromQuiz(quizId, questionId);
            return RedirectToPage("./Details", new { QuizId = quizId });
        }

        public async Task<IActionResult> OnPostAssignToCurrentQuiz(int questionId, int quizId)
        {
            await _svc.AssignQuestionToQuiz(quizId, questionId);
            return RedirectToPage("./Details", new { QuizId = quizId });
        }

        public async Task<IActionResult> OnPostHardDeleteQuestion(int questionId, int quizId)
        {
            await _questionData.DeleteHardAsync(questionId);
            return RedirectToPage("./Details", new { QuizId = quizId });
        }
    }
}