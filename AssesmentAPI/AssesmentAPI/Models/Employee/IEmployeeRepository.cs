using System;
using AssesmentAPI.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssesmentAPI.Models.Employee
{
    public interface IEmployeeRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IEnumerable<Models.Entities.Employee>> Search(string name);
        Task<bool> SaveChangesAsync();
        Task<Models.Entities.Employee[]> getAllEmployeesAsync();
        Task<Models.Entities.Employee> getEmployeeAsync(int id);
        Task<int> getIdByFullname(string name, string surname);
        Task<Models.Entities.Employee[]> getAllEmployeesForManager(int id);
    }
}

