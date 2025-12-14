using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Interfaces.Repository
{
    public interface IUserRoleMappingRepository: IGenericRepository<UserRoleMapping>
    {
        Task AddRole(Guid UserId, RoleEnum roleEnum);

        Task RemoveRole(Guid userId, RoleEnum roleEnum);
    }
}
