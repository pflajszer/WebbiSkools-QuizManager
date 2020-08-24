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
    public class OAnswer : IDbOps<EAnswer>
    {
		private readonly ApplicationDbContext _ctx;
		public OAnswer(ApplicationDbContext ctx)
		{
			_ctx = ctx;
		}

		public async Task<int> AddAsync(EAnswer entity)
		{
			await _ctx.Answers.AddAsync(entity);
			var numOfAddedEntities = await _ctx.SaveChangesAsync();
			return numOfAddedEntities;
		}

		public async Task<int> DeleteHardAsync(int id)
		{
			var answer = await _ctx.Answers
				.FirstOrDefaultAsync(x => x.Id == id);
			_ctx.Answers.Remove(answer);
			var numOfDeletedEntities = await _ctx.SaveChangesAsync();
			return numOfDeletedEntities;
		}

		public async Task<int> DeleteSoftAsync(int id)
		{
			var answer = await _ctx.Answers
				.FirstOrDefaultAsync(x => x.Id == id);
			answer.Deleted = true;
			var numOfDeletedEntities = await _ctx.SaveChangesAsync();
			return numOfDeletedEntities;

		}

		public IQueryable<EAnswer> Get()
		{
			var result = _ctx.Answers
				.AsQueryable();
			return result;
		}

		public async Task<int> UpdateAsync(EAnswer newVersion)
		{
			newVersion.LastModified = DateTime.Now;
			var entity = _ctx.Answers.Find(newVersion.Id);
			_ctx.Entry(entity).CurrentValues.SetValues(newVersion);
			var numOfEditedEntities = await _ctx.SaveChangesAsync();
			return numOfEditedEntities;
		}
	}
}
