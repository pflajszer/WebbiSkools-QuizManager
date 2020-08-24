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
    public class OQuizTests
    {
        ApplicationDbContext _ctx;
        IDbOps<EQuiz> _sut;
        EQuiz _model;
        public OQuizTests()
        {
            _ctx = ContextBuilder.Ctx;
            _sut = new OQuiz(_ctx);
            _model = new EQuiz()
            {
                Name = "Some quiz"
            };
        }

        [Fact, Priority(8)]
        public async Task CantAddNewQuizWithoutQuestions()
        {
            // Arrange:
            // n/a

            // Act:
            var result = await _sut.AddAsync(_model);

            // Assert:
            Assert.Equal(1, result);
        }

		[Fact, Priority(1)]
		public async Task CanAddNewQuizWithQuestions()
		{
			// Arrange:
			var questions = _ctx.Questions.Take(3).ToList();
			_model.Questions = questions;

			// Act:
			var result = await _sut.AddAsync(_model);

			// Assert:
			Assert.Equal(1 + questions.Count, result);
		}


		[Fact, Priority(2)]
        public async Task CanGetAllQuizes()
        {
            // Arrange:
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Act:
            var quizes = await _sut.Get().ToListAsync();

            // serialize JSON to a string and then write string to a file
            File.WriteAllText(@"C:\Users\pawelflajszer\source\repos\QuizManagerTemplate\QuizManagerTemplate.BRL.Tests\DataAccessMediatorsTests\TestData\quizes.json", JsonConvert.SerializeObject(quizes, settings));


            // Assert:
            Assert.NotEmpty(quizes);
        }

        [Fact, Priority(3)]
        public async Task CanUpdateQuiz()
        {
            // Arrange:
            var toUpdate = await _sut.Get().FirstOrDefaultAsync(x => !x.Deleted);

            toUpdate.Name = "Changed Name";

            // Act:
            var result = await _sut.UpdateAsync(toUpdate);

            // Assert:
            Assert.Equal(1, result);
        }

		[Fact, Priority(4)]
		public async Task CanAddQuestionToQuiz()
		{
			// Arrange:
			var toUpdate = await _sut.Get().FirstOrDefaultAsync(x => !x.Deleted);

			toUpdate.Questions.Add(new EQuestion()
			{
				Text = "this has been added directly to quiz"
			});

			// Act:
			var result = await _sut.UpdateAsync(toUpdate);

			// Assert:
			Assert.Equal(2, result);
		}

		[Fact, Priority(5)]
		public async Task CanRemoveQuestionFromQuiz()
		{
			// Arrange:
			var toUpdate = await _sut.Get().FirstOrDefaultAsync(x => x.Questions.Any());

			var question = toUpdate.Questions.FirstOrDefault();

			toUpdate.Questions.Remove(question);

			// Act:
			var result = await _sut.UpdateAsync(toUpdate);

			// Assert:
			Assert.True(result > 0);
		}

		[Fact, Priority(6)]
        public async Task CanSoftDeleteQuiz()
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

		[Fact, Priority(7)]
		public async Task CanHArdDeleteQuiz()
		{
			// Arrange:
			var toDelete = await _sut.Get().FirstOrDefaultAsync();
			int affected = toDelete.Questions.Count();
			// Act:
			var result = await _sut.DeleteHardAsync(toDelete.Id);

			// Assert:
			var deleted = await _sut.Get().FirstOrDefaultAsync(x => x.Id == toDelete.Id);
			Assert.Null(deleted);
			Assert.Equal(1 + affected, result);
		}
	}
}
