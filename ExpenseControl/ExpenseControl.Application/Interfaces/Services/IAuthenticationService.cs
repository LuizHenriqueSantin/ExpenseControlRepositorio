using ExpenseControl.Application.Models;
using ExpenseControl.Application.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
