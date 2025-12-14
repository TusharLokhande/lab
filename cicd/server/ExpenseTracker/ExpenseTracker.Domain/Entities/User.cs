using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Domain.Entities
{
    public class User: BaseEntity
    {
        public Guid Id { get; private set; } 
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? PhoneNumber { get; private set; }

        private readonly List<AuthProvider> _authProviders = new();
        public IReadOnlyCollection<AuthProvider> AuthProviders => _authProviders.AsReadOnly();

        public ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();

        public void AddAuthProvider(AuthProvider provider)
        {
            _authProviders.Add(provider);
        }

        public void AddRole(Guid RoleId)
        {
            if (this.UserRoleMappings.Any(k => k.Role.Id == RoleId)) return;

            var userRole = new UserRoleMapping(
                this.Id,
                RoleId
            );
            this.UserRoleMappings.Add(userRole);
        }

        public User(string name, string email, string phoneNumber = null)
        {
            Id = Guid.NewGuid();   
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
