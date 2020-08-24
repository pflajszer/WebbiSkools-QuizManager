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
	public class QuizDataAccessMediator : IDataAccessMediator<QuizViewModel>
	{
		private readonly IMapper _mapper;
		private readonly IDbOps<EQuiz> _dal;
		//private readonly IHttpContextAccessor _httpCtxAccessor;
		public QuizDataAccessMediator(IMapper mapper, IDbOps<EQuiz> dal/*, IHttpContextAccessor httpCtxAccessor*/)
		{
			_mapper = mapper;
			_dal = dal;
			//_httpCtxAccessor = httpCtxAccessor;
			//CurrentUserId = _httpCtxAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}

		//public string CurrentUserId { get; set; }
		public async Task<int> AddAsync(QuizViewModel entityToAdd)
		{
			//entityToAdd.AuthorId = CurrentUserId;
			var map = _mapper.Map<EQuiz>(entityToAdd);
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

		public async Task<IList<QuizViewModel>> GetAsync()
		{
			var result = _dal.Get();
			var list = await _mapper.ProjectTo<QuizViewModel>(result).ToListAsync();
			return list;
		}

		public async Task<QuizViewModel> GetByIdAsync(int id)
		{
			var result = await _dal.Get().FirstOrDefaultAsync(x => x.Id == id);
			var map = _mapper.Map<QuizViewModel>(result);
			return map;
		}

		public async Task<bool> IsAnyWithIdAsync(int id)
		{
			var result = await _dal.Get().AnyAsync(x => x.Id == id);
			return result;
		}

		public async Task<int> UpdateAsync(QuizViewModel entityToUpdate)
		{
			//entityToUpdate.ModifiedBy = CurrentUserId;
			var map = _mapper.Map<EQuiz>(entityToUpdate);
			var updatedEntities = await _dal.UpdateAsync(map);
			return updatedEntities;
		}
	}
}
