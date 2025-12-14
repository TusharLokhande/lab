using ExpenseTracker.Application.Interfaces.Repository;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class UserRoleMappingRepository: GenericRepository<UserRoleMapping>, IUserRoleMappingRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger<UserRoleMappingRepository> _Logger;

        public UserRoleMappingRepository(
            AppDbContext db,
            ILogger<UserRoleMappingRepository> Logger
        ) : base(db)
        {
            _db = db;
            _Logger = Logger;
        }


        public async Task AddRole(Guid UserId, RoleEnum roleEnum)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                _Logger.LogError("Error while Creating the Role: ",ex);
                throw ex;
            }
        }

        public async Task RemoveRole(Guid userId, RoleEnum roleEnum)
        {
            try
            {
                // Find role record
                var role = await _db.Roles
                    .FirstOrDefaultAsync(r => r.Code == roleEnum.ToString());

                if (role == null)
                    throw new Exception($"Role '{roleEnum}' does not exist.");

                // Check if mapping exists
                var record = await _db.UserRoles
                    .FirstOrDefaultAsync(r => r.UserId == userId && r.RoleId == role.Id);

                if (record == null)
                    return; // Nothing to remove

                // Remove relation
                _db.UserRoles.Remove(record);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _Logger.LogError("Error while removing the Role: ", ex);
                throw ex;
            }
        }

    }
}
