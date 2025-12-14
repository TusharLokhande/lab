using ExpenseTracker.Application.Features.Auth.Shared;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;


namespace ExpenseTracker.Application.Features.Auth.Register
{
    public class RegisterRequest: UserDto
    {
        public string? PhoneNumber { get; set; }
        public AuthProviderRequest AuthProvider { get; set; }
    }

    public class AuthProviderRequest
    {
        public string ProviderId { get; set; } = string.Empty;
        public AuthProviderType AuthProviderType { get; set; }
    }
}
