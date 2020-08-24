using AutoMapper;
using MockQueryable.Moq;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.AutomapperProfiles;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Implementations;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager;
using WebbiSkools.QuizManager.DAL.Operations.Abstractions;
using Xunit;

namespace WebbiSkools.QuizManager.BRL.Tests.DataAccessMediatorsTests
{
	/// <summary>
	/// In order to execute those tests please go to the DAL.Tests project and DatabaseSnapshooter class and provide local file paths for the snapshot to be saved. Then replace the _dataFilePath with the local file containing the json snapshot data.
	/// </summary>
	public class QuestionDataAccessMediatorTests
    {
		private readonly string _dataFilePath = @"C:\Users\pawelflajszer\source\repos\WebbiSkools.QuizManager\WebbiSkools.QuizManager.BRL.Tests\DataAccessMediatorsTests\TestData\questions.json";

		IMapper _mapper;
		Mock<IDbOps<EQuestion>> _dalMock;
		Mock<IQueryable<EQuestion>> _dataMock;
		IDataAccessMediator<QuestionViewModel> _sut;

		public QuestionDataAccessMediatorTests()
		{
			var quizProfile = new QuizProfile();
			var questionProfile = new QuestionProfile();
			var answerProfile = new AnswerProfile();
			var configuration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(quizProfile);
				cfg.AddProfile(questionProfile);
				cfg.AddProfile(answerProfile);
			});

			_mapper = new Mapper(configuration);

			_dalMock = new Mock<IDbOps<EQuestion>>();

			_sut = new QuestionDataAccessMediator(_mapper, _dalMock.Object);

			var json = File.ReadAllText(_dataFilePath);
			var data = JsonConvert.DeserializeObject<IEnumerable<EQuestion>>(json);

			_dataMock = data.AsQueryable().BuildMock();
		}

		[Fact]
		public async Task CanAddMappedQuestion()
		{
			// Arrange:
			var model = new QuestionViewModel();
			model.Text = "Some ques text";

			var mapped = _mapper.Map<EQuestion>(model);

			_dalMock.Setup(x => x.AddAsync(It.IsAny<EQuestion>())).ReturnsAsync(1);

			// Act:
			var result = await _sut.AddAsync(model);

			// Assert:
			_dalMock.Verify(m => m.AddAsync(It.IsAny<EQuestion>()));
			Assert.Equal(1, result);
		}

		[Fact]
		public async Task CanGetAllQuestionsAsynchronously()
		{
			// Arrange:
			_dalMock.Setup(x => x.Get()).Returns(() => _dataMock.Object);

			// Act:
			var result = new List<QuestionViewModel>();
			foreach (var item in await _sut.GetAsync())
			{
				result.Add(item);
			}

			// Assert:
			Assert.NotNull(result);
		}

	}
}
