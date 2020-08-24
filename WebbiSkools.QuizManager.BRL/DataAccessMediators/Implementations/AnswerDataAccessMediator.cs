using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager;
using WebbiSkools.QuizManager.DAL.Operations.Abstractions;

namespace WebbiSkools.QuizManager.BRL.DataAccessMediators.Implementations
{
    public class AnswerDataAccessMediator : IDataAccessMediator<AnswerViewModel>
    {
		private readonly IMapper _mapper;
		private readonly IDbOps<EAnswer> _dal;
		public AnswerDataAccessMediator(IMapper mapper, IDbOps<EAnswer> dal)
		{
			_mapper = mapper;
			_dal = dal;
		}
		public async Task<int> AddAsync(AnswerViewModel entityToAdd)
		{
			var map = _mapper.Map<EAnswer>(entityToAdd);
			var entitiesAdded = await _dal.AddAsync(map);
			return entitiesAdded;
		}

		public async Task<int> DeleteHardAsync(int id)
		{
			return await _dal.DeleteHardAsync(id);
		}

		public async Task<int> DeleteSoftAsync(int id)
		{
			return await _dal.DeleteSoftAsync(id);
		}

		public async Task<IList<AnswerViewModel>> GetAsync()
		{
			var result = _dal.Get();
			var list = await _mapper.ProjectTo<AnswerViewModel>(result).ToListAsync();
			return list;
		}

		public async Task<AnswerViewModel> GetByIdAsync(int id)
		{
			var result = await _dal.Get().FirstOrDefaultAsync(x => x.Id == id);
			var map = _mapper.Map<AnswerViewModel>(result);
			return map;
		}

		public async Task<bool> IsAnyWithIdAsync(int id)
		{
			var result = await _dal.Get().AnyAsync(x => x.Id == id);
			return result;
		}

		public async Task<int> UpdateAsync(AnswerViewModel entityToUpdate)
		{
			var map = _mapper.Map<EAnswer>(entityToUpdate);
			var updatedEntities = await _dal.UpdateAsync(map);
			return updatedEntities;
		}
	}
}
