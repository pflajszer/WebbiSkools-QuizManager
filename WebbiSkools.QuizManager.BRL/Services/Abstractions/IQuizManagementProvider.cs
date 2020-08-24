using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;

namespace WebbiSkools.QuizManager.BRL.Services.Abstractions
{
	public interface IQuizManagementProvider
	{
		Task<int> UnassignQuestionFromQuiz(int quizId, int questionId);
		Task<int> AssignQuestionToQuiz(int quizId, int questionId);
		Task<SelectList> GenerateQuizesDropdown(bool selectEmpty = false, string specificIdToSelect = null);
	}
}