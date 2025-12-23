using ExpenseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAllAsQueryable();
        Task<bool> AddAsync(Category obj);
        Task<bool> DeleteAsync(Category obj);
        Task<bool> UpdateAsync(Category obj);
    }
}
