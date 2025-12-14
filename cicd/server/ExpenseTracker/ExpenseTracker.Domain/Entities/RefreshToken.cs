using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class RefreshToken: BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; } = default!;
        public DateTime ExpiresAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public override bool IsActive => RevokedAt == null && !IsExpired;

        private RefreshToken() 
        { 
        
        } 

        public RefreshToken(Guid userId, string token, DateTime expiresAt)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
            CreatedAt = DateTime.UtcNow;
        }

        public void Revoke()
        {
            if (!IsActive) return;
            RevokedAt = DateTime.UtcNow;
        }
    }
}
