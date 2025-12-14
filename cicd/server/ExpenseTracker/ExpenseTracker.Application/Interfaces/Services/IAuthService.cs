using ExpenseTracker.Application.Common;
using ExpenseTracker.Application.Features.Auth.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Interfaces.Services
{
    public interface IAuthService
    {

        Task<Result<RegisterResponse>> AuthenticateUser(RegisterRequest request);
    }
}
