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
using Xunit;

namespace WebbiSkools.QuizManager.DAL.Tests.Helpers
{
    public class DatabaseSnapshooter
    {
		/// <summary>
		/// Use this only when you updated the database schema and want to test the new one.
		/// In this case, just comment out the 'Skip' parameter with the brackets from the Fact attribute below.
		/// </summary>
		/// <returns></returns>
		[Fact/*(Skip = "Not a test - Database seed")*/]
		public async Task SeedTheTestDataWithCurrentDatabaseState()
		{
			string path = "";
			path = System.AppContext.BaseDirectory;
			//await GetDatabaseSnapshot();
		}

		public static string BasePath { get; set; }
			= @"C:\Users\pawelflajszer\source\repos\WebbiSkools.QuizManager\WebbiSkools.QuizManager.BRL.Tests\DataAccessMediatorsTests\TestData";

		private static async Task GetDatabaseSnapshot()
		{
			var _ctx = ContextBuilder.Ctx;
			var _quizesDb = new OQuiz(_ctx);
			var _questionsDb = new OQuestion(_ctx);
			var _answersDb = new OAnswer(_ctx);

			var answers = await _answersDb.Get().ToListAsync();
			File.WriteAllText(@$"{BasePath}\answers.json",
				JsonConvert.SerializeObject(answers));

			var questions = await _questionsDb.Get().ToListAsync();
			File.WriteAllText(@$"{BasePath}\questions.json",
				JsonConvert.SerializeObject(questions));

			var quizes = await _quizesDb.Get().ToListAsync();
			File.WriteAllText(@$"{BasePath}\quizes.json",
				JsonConvert.SerializeObject(quizes));
		}


	}
}
