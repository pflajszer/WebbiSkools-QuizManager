using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.DAL.Context;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager;
using WebbiSkools.QuizManager.DAL.Operations.Abstractions;
using WebbiSkools.QuizManager.DAL.Operations.Implementations;
using WebbiSkools.QuizManager.DAL.Tests.Helpers;
using Xunit;
using Xunit.Priority;

namespace WebbiSkools.QuizManager.DAL.Tests.OperationsTests
{
    public class OQuestionTests
    {
        ApplicationDbContext _ctx;
        IDbOps<EQuestion> _sut;
        EQuestion _model;
        public OQuestionTests()
        {
            _ctx = ContextBuilder.Ctx;
            _sut = new OQuestion(_ctx);
            _model = new EQuestion()
            {
                Text = "Thjis is question text"
            };
        }

        [Fact, Priority(0)]
        public async Task CanAddNewQuestionWithoutQuiz()
        {
            // Arrange:
            // n/a

            // Act:
            var result = await _sut.AddAsync(_model);

            // Assert:
            Assert.Equal(1, result);
        }

		[Fact, Priority(1)]
		public async Task CanAddNewQuestionWithQuiz()
		{
			// Arrange:
			var quiz = _ctx.Quizes.FirstOrDefault();
			if (quiz is null)
			{
				throw new Exception("Seed the database - no quizes available");
			}
			_model.QuizId = quiz.Id;
			//_model.Question = question;

			// Act:
			var result = await _sut.AddAsync(_model);

			// Assert:
			Assert.Equal(1, result);
		}

        [Fact, Priority(1)]
        public async Task CanAddNewQuestionWithAnswers()
        {
            // Arrange:
            var a1 = new EAnswer();
            a1.Text = "some answer";

            var a2 = new EAnswer();
            a2.Text = "some other answer";

            var a3 = new EAnswer();
            a3.Text = "yet another answer";
            _model.Answers.Add(a1);
            _model.Answers.Add(a2);
            _model.Answers.Add(a3);

            // Act:
            var result = await _sut.AddAsync(_model);

            // Assert:
            Assert.Equal(1 + _model.Answers.Count, result);
        }

        [Fact, Priority(1)]
		public async Task CanAddNewQuestionWithQuizIdOnly()
		{
			// Arrange:
			var quiz = _ctx.Quizes.FirstOrDefault();
			if (quiz is null)
			{
				throw new Exception("Seed the database - no quizes available");
			}
			_model.QuizId = quiz.Id;
			//_model.Question = question;

			// Act:
			var result = await _sut.AddAsync(_model);

			// Assert:
			Assert.Equal(1, result);
		}


		[Fact, Priority(2)]
        public async Task CanGetAllQuestions()
        {
            // Arrange:
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Act:
            var questions = await _sut.Get().ToListAsync();

            // serialize JSON to a string and then write string to a file
            File.WriteAllText(@"C:\Users\pawelflajszer\source\repos\QuizManagerTemplate\QuizManagerTemplate.BRL.Tests\DataAccessMediatorsTests\TestData\questions.json", JsonConvert.SerializeObject(questions, settings));


            // Assert:
            Assert.NotEmpty(questions);
        }

        [Fact(Skip ="Only use if you need a specific question. Otherwise this test will fail if someone deletes this particular ID in tests."), Priority(2)]
        
        public async Task CanGetASpecificQuestion()
        {
            // Arrange:

            // Act:
            var questions = _sut.Get().FirstOrDefault(x => x.Id == 33);



            // Assert:
            Assert.NotEmpty(questions.Answers);
            Assert.NotNull(questions.Quiz);
        }

        [Fact, Priority(3)]
        public async Task CanUpdateQuestion()
        {
            // Arrange:
            var toUpdate = await _sut.Get().FirstOrDefaultAsync(x => !x.Deleted);
            var x = new List<EQuiz>();

            toUpdate.Text = "Changed question";

            // Act:
            var result = await _sut.UpdateAsync(toUpdate);

            // Assert:
            Assert.Equal(1, result);
        }

        [Fact, Priority(4)]
        public async Task CanSoftDeleteQuestion()
        {
            // Arrange:
            var toDelete = await _sut.Get().FirstOrDefaultAsync(x => !x.Deleted);

            // Act:
            var result = await _sut.DeleteSoftAsync(toDelete.Id);

            // Assert:
            var deleted = await _sut.Get().FirstOrDefaultAsync(x => x.Id == toDelete.Id);
            Assert.True(deleted.Deleted);
            Assert.Equal(1, result);
        }

		[Fact, Priority(5)]
		public async Task CanHArdDeleteQuestion()
		{
			// Arrange:
			var toDelete = await _sut.Get().FirstOrDefaultAsync();
			int affected = toDelete.Answers.Count();

			// Act:
			var result = await _sut.DeleteHardAsync(toDelete.Id);

			// Assert:
			var deleted = await _sut.Get().FirstOrDefaultAsync(x => x.Id == toDelete.Id);
			Assert.Null(deleted);
			Assert.Equal(1 + affected, result);
		}

        [Fact, Priority(5)]
        public async Task CanHArdDeleteQuestionWithAnswers()
        {
            // Arrange:
            var toDelete = await _sut.Get().FirstOrDefaultAsync(x => x.Answers.Any());
            int affected = toDelete.Answers.Count();

            // Act:
            var result = await _sut.DeleteHardAsync(toDelete.Id);

            // Assert:
            var deleted = await _sut.Get().FirstOrDefaultAsync(x => x.Id == toDelete.Id);
            Assert.Null(deleted);
            Assert.Equal(1 + affected, result);
        }
    }
}
