using ExpenseTracker.Application.Interfaces.Repository;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class RoleRepository:  GenericRepository<Role>, IRoleRepository
    {
        private readonly AppDbContext _db;

        public RoleRepository(AppDbContext _db): base(_db)
        {
            this._db = _db;
        }


        public async Task<Role> GetRoleByCode(RoleEnum role)
        {
            return await _db.Roles.Where(k => k.Code == role.ToString()).FirstAsync();
        }
    }
}
