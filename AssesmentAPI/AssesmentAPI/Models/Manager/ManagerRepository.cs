using System;
using AssesmentAPI.Models.Employee;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AssesmentAPI.Models.Manager
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly AppDbContext _appDbContext;

        public ManagerRepository(AppDbContext appDbContext)
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

        public async Task<IEnumerable<Models.Entities.Manager>> Search(string name)
        {
            IQueryable<Models.Entities.Manager> query = _appDbContext.managers;

            if (!string.IsNullOrEmpty(name)) // if there is anything
            {
                query = query.Where(e => e.name.Contains(name) || e.surname.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<Models.Entities.Manager[]> getAllManagersAsync()
        {
            IQueryable<Models.Entities.Manager> query = _appDbContext.managers;
            return await query.ToArrayAsync();
        }

        public async Task<Models.Entities.Manager> getManagerAsync(int id)
        {
            IQueryable<Models.Entities.Manager> query = _appDbContext.managers.Where(zz => zz.ManagerID == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> getIdByName(string name)
        {
            IQueryable<Entities.Manager> query = _appDbContext.managers.Where(zz => zz.name == name);
            var results = query.Select(zz => zz.ManagerID);

            return await results.FirstOrDefaultAsync();
        }




        public async Task<bool> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}

