using ExpenseControl.Application.Models.Helpers;
using ExpenseControl.Application.Models.In;
using ExpenseControl.Application.Models.Out;
using ExpenseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        List<CategoryOut> GetAll();
        CategoryOut GetById(Guid id);
        List<EntitySelect> GetForSelect(TransactionType type);
        Task<bool> CreateAsync(CategoryIn category);
        Task<bool> UpdateAsync(CategoryIn category);
        Task<bool> DeleteAsync(Guid id);
    }
}
