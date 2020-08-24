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
	public class AnswerDataAccessMediatorTests
    {
		private string _dataFilePath = @"C:\Users\pawelflajszer\source\repos\WebbiSkools.QuizManager\WebbiSkools.QuizManager.BRL.Tests\DataAccessMediatorsTests\TestData\answers.json";


		IMapper _mapper;
		Mock<IDbOps<EAnswer>> _dalMock;
		Mock<IQueryable<EAnswer>> _dataMock;
		IDataAccessMediator<AnswerViewModel> _sut;

		public AnswerDataAccessMediatorTests()
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

			_dalMock = new Mock<IDbOps<EAnswer>>();

			_sut = new AnswerDataAccessMediator(_mapper, _dalMock.Object);

			var json = File.ReadAllText(_dataFilePath);
			var data = JsonConvert.DeserializeObject<IEnumerable<EAnswer>>(json);

			_dataMock = data.AsQueryable().BuildMock();
		}

		[Fact]
		public async Task CanAddMappedAnswer()
		{
			// Arrange:
			var model = new AnswerViewModel();
			model.Text = "Text of view model answer";
			model.IsCorrect = false;

			var mapped = _mapper.Map<EAnswer>(model);

			_dalMock.Setup(x => x.AddAsync(It.IsAny<EAnswer>())).ReturnsAsync(1);

			// Act:
			var result = await _sut.AddAsync(model);

			// Assert:
			_dalMock.Verify(m => m.AddAsync(It.IsAny<EAnswer>()));
			Assert.Equal(1, result);
		}

		[Fact]
		public async Task CanGetAllAnswers()
		{
			// Arrange:
			_dalMock.Setup(x => x.Get()).Returns(() => _dataMock.Object);

			// Act:
			var result = new List<AnswerViewModel>();
			foreach (var item in await _sut.GetAsync())
			{
				result.Add(item);
			}

			// Assert:
			Assert.NotNull(result);
		}
	}
}
