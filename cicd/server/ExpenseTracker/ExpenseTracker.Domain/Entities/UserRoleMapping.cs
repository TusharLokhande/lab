using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class UserRoleMapping: BaseEntity
    {

        public UserRoleMapping() { 
        
        }

        public UserRoleMapping(Guid UserId, Guid RoleId)
        {
            Id = Guid.NewGuid();
            this.UserId = UserId;
            this.RoleId = RoleId;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

    }
}
