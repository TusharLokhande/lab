using ExpenseTracker.Application.Interfaces.Repository;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _db.Users
                .Include(x => x.UserRoleMappings)
                    .ThenInclude(x => x.Role)
                .Include(x => x.AuthProviders)
                .FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

    }
}
