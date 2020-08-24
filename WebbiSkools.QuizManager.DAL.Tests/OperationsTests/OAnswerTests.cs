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
    public class OAnswerTests
    {
        ApplicationDbContext _ctx;
        IDbOps<EAnswer> _sut;
        EAnswer _model;
        public OAnswerTests()
        {
            _ctx = ContextBuilder.Ctx;
            _sut = new OAnswer(_ctx);
            _model = new EAnswer()
            {
                Text = "I am an another answer",
                IsCorrect = false
            };
        }

        [Fact, Priority(0)]
        public async Task CannotAddAnswerWithoutQuestion()
        {
            // Arrange:
            // n/a

            // Act:
            //var result = await _sut.AddAsync(_model);

            // Assert:
            await Assert.ThrowsAnyAsync<DbUpdateException>(async () => await _sut.AddAsync(_model));
            //Assert.Equal(1, result);
        }

		[Fact, Priority(1)]
		public async Task CanAddAnswerWithQuestion()
		{
			// Arrange:
			var question = _ctx.Questions.FirstOrDefault();
			if (question is null)
			{
				throw new Exception("Seed the database - no questions available");
			}
			_model.Question = question;

			// Act:
			var result = await _sut.AddAsync(_model);

			// Assert:
			Assert.Equal(1, result);
		}


		[Fact, Priority(1)]
		public async Task CanAddAnswerWithQuestionIdOnly()
		{
			// Arrange:
			var question = _ctx.Questions.FirstOrDefault();
			if (question is null)
			{
				throw new Exception("Seed the database - no questions available");
			}
			_model.Question = question;

			// Act:
			var result = await _sut.AddAsync(_model);

			// Assert:
			Assert.Equal(1, result);
		}

		[Fact, Priority(2)]
        public async Task CanGetAllAnswers()
        {
            // Arrange:
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Act:
            var answers = await _sut.Get().ToListAsync();
            File.WriteAllText(@"C:\Users\pawelflajszer\source\repos\QuizManagerTemplate\QuizManagerTemplate.BRL.Tests\DataAccessMediatorsTests\TestData\answers.json",
                JsonConvert.SerializeObject(answers, settings));

            // Assert:
            Assert.NotEmpty(answers);
        }

        [Fact, Priority(3)]
        public async Task CanUpdateAnswer()
        {
            // Arrange:
            var toUpdate = await _sut.Get().FirstOrDefaultAsync(x => !x.Deleted);
            var x = new List<EAnswer>();

            toUpdate.Text = "Changed Answer";

            // Act:
            var result = await _sut.UpdateAsync(toUpdate);

            // Assert:
            Assert.Equal(1, result);
        }

        [Fact, Priority(4)]
        public async Task CanSoftDeleteAnswer()
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
        public async Task CanHArdDeleteAnswer()
        {
            // Arrange:
            var toDelete = await _sut.Get().FirstOrDefaultAsync();

            // Act:
            var result = await _sut.DeleteHardAsync(toDelete.Id);

            // Assert:
            var deleted = await _sut.Get().FirstOrDefaultAsync(x => x.Id == toDelete.Id);
            Assert.Null(deleted);
            Assert.Equal(1, result);
        }
    }
}
