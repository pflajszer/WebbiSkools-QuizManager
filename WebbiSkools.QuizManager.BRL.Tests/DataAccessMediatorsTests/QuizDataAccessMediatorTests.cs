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
	public class QuizDataAccessMediatorTests
    {
			 
		private readonly string _dataFilePath = @"C:\Users\pawelflajszer\source\repos\WebbiSkools.QuizManager\WebbiSkools.QuizManager.BRL.Tests\DataAccessMediatorsTests\TestData\quizes.json";

		private IMapper _mapper;
		private Mock<IDbOps<EQuiz>> _dalMock;
		private Mock<IQueryable<EQuiz>> _dataMock;
		//private Mock<IHttpContextAccessor> _httpCtxAccessorMock;
		private IDataAccessMediator<QuizViewModel> _sut;

		public QuizDataAccessMediatorTests()
		{
			var quizProfile = new QuizProfile();
			var questionProfile = new QuestionProfile();
			var answerProfile = new AnswerProfile();
			var configuration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(quizProfile);
				cfg.AddProfile(questionProfile);
				cfg.AddProfile(answerProfile);
				cfg.ForAllMaps((tm, me) => me.ForAllMembers(option => option.Condition((source, destination, sourceMember) => sourceMember != null)));
			});

			_mapper = new Mapper(configuration);

			_dalMock = new Mock<IDbOps<EQuiz>>();

			//_httpCtxAccessorMock = new Mock<IHttpContextAccessor>();

			_sut = new QuizDataAccessMediator(_mapper, _dalMock.Object/*, _httpCtxAccessorMock.Object*/);

			var json = File.ReadAllText(_dataFilePath);
			var data = JsonConvert.DeserializeObject<IEnumerable<EQuiz>>(json);

			_dataMock = data.AsQueryable().BuildMock();
		}

		[Fact]
		public async Task CanAddMappedQuiz()
		{
			// Arrange:
			var model = new QuizViewModel();
			model.Name = "New QUiz test";

			var mapped = _mapper.Map<EQuiz>(model);

			_dalMock.Setup(x => x.AddAsync(It.IsAny<EQuiz>())).ReturnsAsync(1);

			// Act:
			var result = await _sut.AddAsync(model);

			// Assert:
			_dalMock.Verify(m => m.AddAsync(It.IsAny<EQuiz>()));
			Assert.Equal(1, result);
		}

		[Fact]
		public async Task CanGetAllQuizesAsynchronously()
		{
			// Arrange:
			_dalMock.Setup(x => x.Get()).Returns(() => _dataMock.Object);

			// Act:
			var result = new List<QuizViewModel>();
			foreach (var item in await _sut.GetAsync())
			{
				result.Add(item);
			}


			// Assert:
			Assert.NotNull(result);
		}
	}
}
