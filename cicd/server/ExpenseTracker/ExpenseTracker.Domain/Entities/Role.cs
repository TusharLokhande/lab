using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class Role: BaseEntity
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();

    }
}
