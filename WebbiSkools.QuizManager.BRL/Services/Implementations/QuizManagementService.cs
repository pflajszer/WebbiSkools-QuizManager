using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions;
using WebbiSkools.QuizManager.BRL.Services.Abstractions;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;

namespace WebbiSkools.QuizManager.BRL.Services.Implementations
{
	public class QuizManagementService : IQuizManagementProvider
	{
		IDataAccessMediator<QuizViewModel> _quiz;
		IDataAccessMediator<QuestionViewModel> _question;
		IDataAccessMediator<AnswerViewModel> _answer;
		public QuizManagementService(IDataAccessMediator<QuizViewModel> quiz,
									 IDataAccessMediator<QuestionViewModel> question,
									 IDataAccessMediator<AnswerViewModel> answer)
		{
			_quiz = quiz;
			_question = question;
			_answer = answer;
		}

		public async Task<int> UnassignQuestionFromQuiz(int quizId, int questionId)
		{
			var question = await _question.GetByIdAsync(questionId);
			var quiz = await _quiz.GetByIdAsync(quizId);
			if (quiz != null && question != null)
			{
				var questionToRemove = quiz.Questions.FirstOrDefault(x => x.Id == questionId);
				if (questionToRemove != null)
				{
					question.Quiz = null;
					question.QuizId = null;
					return await _question.UpdateAsync(question);
				}
			}
			return 0;
		}

		public async Task<int> AssignQuestionToQuiz(int quizId, int questionId)
		{
			var question = await _question.GetByIdAsync(questionId);
			var quiz = await _quiz.GetByIdAsync(quizId);
			if (quiz != null && question != null)
			{
				var questionToAdd = quiz.Questions.FirstOrDefault(x => x.Id == questionId);
				if (questionToAdd is null)
				{
					question.Quiz = quiz;
					question.QuizId = quiz.Id;
					return await _question.UpdateAsync(question);
				}
			}
			return 0;
		}

		public async Task<SelectList> GenerateQuizesDropdown(bool selectEmpty = false, string specificIdToSelect = null)
		{
			var allItems = await _quiz.GetAsync();
			var emptyQuiz = new QuizViewModel()
			{
				Id = -1,
				Name = "*** Leave this for no Quiz assignment ***"
			};
			allItems.Add(emptyQuiz);
			var quizesDropdownValues = new SelectList(allItems, "Id", "Name");
			if (selectEmpty)
			{
				var selected = quizesDropdownValues.Where(x => x.Value == "-1").First();
				selected.Selected = true;
			}
			if(specificIdToSelect != null)
			{
				var selected = quizesDropdownValues.Where(x => x.Value == specificIdToSelect).First();
				selected.Selected = true;
			}

			return quizesDropdownValues;
		}
	}
}
