using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.DAL.Context;

namespace WebbiSkools.QuizManager.DAL.Tests.Helpers
{
    public class ContextBuilder
    {
		public static ApplicationDbContext Ctx
		{
			get
			{
				var options = new DbContextOptionsBuilder<ApplicationDbContext>()
					.UseSqlServer("Available in Project Outputs and Deliverables.docx file",
						sqlServerOptionsAction: sqlOptions =>
						{
							sqlOptions.MigrationsAssembly("WebbiSkools.QuizManager.DAL");
							sqlOptions.EnableRetryOnFailure(
							maxRetryCount: 10,
							maxRetryDelay: TimeSpan.FromSeconds(30),
							errorNumbersToAdd: null);
						})
				.Options;
				return new ApplicationDbContext(options);
			}
		}
	}
}
