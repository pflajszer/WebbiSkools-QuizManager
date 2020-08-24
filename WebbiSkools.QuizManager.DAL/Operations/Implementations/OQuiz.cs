using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.DAL.Context;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager;
using WebbiSkools.QuizManager.DAL.Operations.Abstractions;

namespace WebbiSkools.QuizManager.DAL.Operations.Implementations
{
    public class OQuiz : IDbOps<EQuiz>
    {
		private readonly ApplicationDbContext _ctx;
		public OQuiz(ApplicationDbContext ctx)
		{
			_ctx = ctx;
		}

		public async Task<int> AddAsync(EQuiz entity)
		{
			await _ctx.Quizes.AddAsync(entity);
			var numOfAddedEntities = await _ctx.SaveChangesAsync();
			return numOfAddedEntities;
		}

		public async Task<int> DeleteHardAsync(int id)
		{
			var quiz = await _ctx.Quizes
				.FirstOrDefaultAsync(x => x.Id == id);
			_ctx.Quizes.Remove(quiz);
			var numOfDeletedEntities = await _ctx.SaveChangesAsync();
			return numOfDeletedEntities;
		}

		public async Task<int> DeleteSoftAsync(int id)
		{
			var quiz = await _ctx.Quizes
				.FirstOrDefaultAsync(x => x.Id == id);
			quiz.Deleted = true;
			var numOfDeletedEntities = await _ctx.SaveChangesAsync();
			return numOfDeletedEntities;

		}

		public IQueryable<EQuiz> Get()
		{
			var result = _ctx.Quizes
				.Include(quiz => quiz.Questions)
					.ThenInclude(question => question.Answers)
				.AsQueryable();
			return result;
		}

		public async Task<int> UpdateAsync(EQuiz newVersion)
		{
			newVersion.LastModified = DateTime.Now;
			var entity = _ctx.Quizes.Find(newVersion.Id);
			_ctx.Entry(entity).CurrentValues.SetValues(newVersion);
			var numOfEditedEntities = await _ctx.SaveChangesAsync();
			return numOfEditedEntities;
		}
	}
}
