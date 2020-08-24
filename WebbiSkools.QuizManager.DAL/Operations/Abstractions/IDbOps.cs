using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbiSkools.QuizManager.DAL.Operations.Abstractions
{
    public interface IDbOps<T>
    {
        public IQueryable<T> Get();
        public Task<int> UpdateAsync(T entityToUpdate);
        public Task<int> DeleteSoftAsync(int id);
        public Task<int> DeleteHardAsync(int id);
        public Task<int> AddAsync(T entityToAdd);
    }
}
