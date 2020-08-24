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
    public class OQuestion : IDbOps<EQuestion>
    {
		private readonly ApplicationDbContext _ctx;
		public OQuestion(ApplicationDbContext ctx)
		{
			_ctx = ctx;
		}

		public async Task<int> AddAsync(EQuestion entity)
		{
			await _ctx.Questions.AddAsync(entity);
			var numOfAddedEntities = await _ctx.SaveChangesAsync();
			return numOfAddedEntities;
		}

		public async Task<int> DeleteHardAsync(int id)
		{
			var question = await _ctx.Questions
				.FirstOrDefaultAsync(x => x.Id == id);
			_ctx.Questions.Remove(question);
			var numOfDeletedEntities = await _ctx.SaveChangesAsync();
			return numOfDeletedEntities;
		}

		public async Task<int> DeleteSoftAsync(int id)
		{
			var question = await _ctx.Questions
				.FirstOrDefaultAsync(x => x.Id == id);
			question.Deleted = true;
			var numOfDeletedEntities = await _ctx.SaveChangesAsync();
			return numOfDeletedEntities;

		}

		public IQueryable<EQuestion> Get()
		{
			var result = _ctx.Questions
				.Include(x => x.Answers)
				.Include(x => x.Quiz)
				.AsQueryable();
			return result;
		}

		public async Task<int> UpdateAsync(EQuestion newVersion)
		{
			newVersion.LastModified = DateTime.Now;
			var entity = _ctx.Questions.Find(newVersion.Id);
			_ctx.Entry(entity).CurrentValues.SetValues(newVersion);

			var numOfEditedEntities = await _ctx.SaveChangesAsync();
			return numOfEditedEntities;
		}
	}
}
