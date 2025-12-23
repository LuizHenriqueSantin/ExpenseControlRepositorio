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
    public interface ITransactionService
    {
        List<TransactionOut> GetAll();
        TransactionOut GetById(Guid id);
        Task<bool> CreateAsync(TransactionIn transaction);
        Task<bool> UpdateAsync(TransactionIn transaction);
        Task<bool> DeleteAsync(Guid id);
    }
}
