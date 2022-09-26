using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssesmentAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssesmentAPI.Models.Employee
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }

        public void Add<T>(T entity) where T : class
        {
            _appDbContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _appDbContext.Remove(entity);
        }

        public async Task<IEnumerable<Models.Entities.Employee>> Search(string name)
        {
            IQueryable<Models.Entities.Employee> query = _appDbContext.Employees;

            if (!string.IsNullOrEmpty(name)) // if there is anything
            {
                query = query.Where(e => e.name.Contains(name) || e.surname.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<Models.Entities.Employee[]> getAllEmployeesAsync()
        {
            IQueryable<Models.Entities.Employee> query = _appDbContext.Employees;
            return await query.ToArrayAsync();
        }

        public async Task<Models.Entities.Employee[]> getAllEmployeesForManager(int id)
        {
            IQueryable<Models.Entities.Employee> query = _appDbContext.Employees;
            query = query.Where(e => e.ManagerID == id);
            return await query.ToArrayAsync();
        }




        public async Task<Models.Entities.Employee> getEmployeeAsync(int id)
        {
            IQueryable<Models.Entities.Employee> query = _appDbContext.Employees.Where(zz => zz.employeeNumber == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }




        public async Task<int> getIdByFullname(string name, string surname)
        {
            IQueryable<Entities.Employee> query = _appDbContext.Employees.Where(zz => zz.name == name && zz.surname == surname);
            var results = query.Select(zz => zz.employeeNumber);

            return await results.FirstOrDefaultAsync();
        }






    }
}

