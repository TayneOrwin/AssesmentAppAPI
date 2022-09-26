using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssesmentAPI.Models.AccessRole
{
    public interface IAccessRoleRepository
    {

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IEnumerable<Models.Entities.AccessRole>> Search(string name);
        Task<bool> SaveChangesAsync();

        Task<Models.Entities.AccessRole[]> getAllRolesAsync();
        Task<Models.Entities.AccessRole> getRoleAsync(string name);
        Task<int> getIdByAccessRole(string accessRole);


    }
}

