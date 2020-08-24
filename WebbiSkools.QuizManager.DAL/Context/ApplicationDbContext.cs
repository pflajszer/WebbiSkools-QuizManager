using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebbiSkools.QuizManager.DAL.Entities.Identity;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager;
using WebbiSkools.QuizManager.DAL.Extensions;

namespace WebbiSkools.QuizManager.DAL.Context
{
	public class ApplicationDbContext : IdentityDbContext<QuizManagerUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<EQuestion> Questions { get; set; }
		public DbSet<EQuiz> Quizes { get; set; }
		public DbSet<EAnswer> Answers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.CreatedOnDefaultValue<EQuiz>();
			modelBuilder.CreatedOnDefaultValue<EQuestion>();
			modelBuilder.CreatedOnDefaultValue<EAnswer>();

			modelBuilder.LastModifiedOnUpdateValue<EQuiz>();
			modelBuilder.LastModifiedOnUpdateValue<EQuestion>();
			modelBuilder.LastModifiedOnUpdateValue<EAnswer>();

		}
	}
}