using ExpenseControl.Application.Models.Helpers;
using ExpenseControl.Application.Models.In;
using ExpenseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateAsync(UserIn user);
        Task<bool> DeleteCurrentUserAsync();
    }
}
