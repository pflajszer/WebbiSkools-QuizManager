using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.DAL.Context;
using WebbiSkools.QuizManager.DAL.Tests.Helpers;
using Xunit;

namespace WebbiSkools.QuizManager.DAL.Tests.ContextTests
{
    public class ApplicationDbContextTests
    {
		ApplicationDbContext _sut;

		public ApplicationDbContextTests()
		{
			_sut = ContextBuilder.Ctx;
		}

		[Fact]
		public async Task CanConnectToDb()
		{
			bool success = false;
			try
			{
				using (_sut)
				{
					success = await _sut.Database.CanConnectAsync();
				}
			}
			catch (Exception)
			{
				Assert.True(false);
			}

			Assert.True(success);
		}
	}
}
