using ExpenseTracker.Application.Interfaces.Repository;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class AuthProviderRepository: GenericRepository<AuthProvider>, IAuthProviderRepository
    {
        private readonly AppDbContext _db;

        public AuthProviderRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        

    }
}
