using ExpenseTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class AuthProvider: BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public AuthProviderType Provider { get; private set; }
        public string ProviderUserId { get; private set; }
        public User User { get; private set; }

        public AuthProvider()
        {
             
        }

        public AuthProvider(Guid UserId, string ProviderUserId, AuthProviderType provider)
        {
            Id = Guid.NewGuid();
            UserId = UserId;
            Provider = provider;
            ProviderUserId = ProviderUserId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
