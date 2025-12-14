using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Features.Auth.Register
{
    public class RegisterResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
    }
}
