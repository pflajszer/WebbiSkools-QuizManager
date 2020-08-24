using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager.Base;

namespace WebbiSkools.QuizManager.DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
		public static void CreatedOnDefaultValue<T>(this ModelBuilder modelBuilder) where T : QuizElementBase
		{
			modelBuilder.Entity<T>()
				.Property(b => b.CreatedOn)
				.HasDefaultValueSql("getdate()");
		}

		public static void LastModifiedOnUpdateValue<T>(this ModelBuilder modelBuilder) where T : QuizElementBase
		{
			modelBuilder.Entity<T>()
				.Property(b => b.LastModified)
				.ValueGeneratedNever();
		}
	}
}
