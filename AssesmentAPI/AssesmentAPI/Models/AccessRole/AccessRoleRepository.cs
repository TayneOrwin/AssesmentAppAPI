using System;
using static AssesmentAPI.Models.AccessRole.AccessRoleRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AssesmentAPI.Models.AccessRole
{
        public class AccessRoleRepository : IAccessRoleRepository
        {
            private readonly AppDbContext _appDbContext;

            public AccessRoleRepository(AppDbContext appDbContext)
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

            public async Task<Entities.AccessRole[]> getAllRolesAsync()
            {
                IQueryable<Entities.AccessRole> query = _appDbContext.accessRoles;
                return await query.ToArrayAsync();
            }



            public async Task<Entities.AccessRole> getRoleAsync(string name)
            {
                IQueryable<Entities.AccessRole> query = _appDbContext.accessRoles.Where(c => c.RoleDescription == name);
                return await query.FirstOrDefaultAsync();
            }

            public async Task<bool> SaveChangesAsync()
            {
                return await _appDbContext.SaveChangesAsync() > 0;
            }

            public async Task<IEnumerable<Entities.AccessRole>> Search(string name)
            {
                IQueryable<Entities.AccessRole> query = _appDbContext.accessRoles;

                if (!string.IsNullOrEmpty(name)) // if there is anything
                {
                    query = query.Where(e => e.RoleDescription.Contains(name));
                }

                return await query.ToListAsync();
            }



        public async Task<int> getIdByAccessRole(string accessRole)
        {
            IQueryable<Entities.AccessRole> query = _appDbContext.accessRoles.Where(zz => zz.RoleDescription == accessRole);
            var results = query.Select(zz => zz.AccessRoleID);

            return await results.FirstOrDefaultAsync();
        }






    }











}



