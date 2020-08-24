using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbiSkools.QuizManager.BRL.DataAccessMediators.Abstractions
{
    public interface IDataAccessMediator<T>
    {
        public Task<IList<T>> GetAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<bool> IsAnyWithIdAsync(int id);
        public Task<int> UpdateAsync(T entityToUpdate);
        public Task<int> DeleteSoftAsync(int id);
        public Task<int> DeleteHardAsync(int id);
        public Task<int> AddAsync(T entityToAdd);
    }
}
