using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssesmentAPI.Models.Manager
{
    public interface IManagerRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IEnumerable<Models.Entities.Manager>> Search(string name);
        Task<bool> SaveChangesAsync();
        Task<Models.Entities.Manager[]> getAllManagersAsync();
        Task<Models.Entities.Manager> getManagerAsync(int id);
        Task<int> getIdByName(string name);
    }
}

