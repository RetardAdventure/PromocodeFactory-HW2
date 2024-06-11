using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity
    {
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<T> CreateAsync(T data)
        {
            Data = Data.Append(data);
            return Task.FromResult(data);
        }

        public Task<T> UpdateAsync(T data) {
            if (Data.FirstOrDefault(x=>x.Id == data.Id) != null)
            {
                Data = Data.Where(x => x.Id != data.Id).Append(data);
                return Task.FromResult(data);
            }
            throw new Exception(message: "Guid not found");
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            if (Data.FirstOrDefault(x => x.Id == id) != null)
            {
                Data = Data.Where(x => x.Id != id);
                return Task.FromResult(true);
            }
            throw new Exception(message: "Guid not found");
        }

    }
}