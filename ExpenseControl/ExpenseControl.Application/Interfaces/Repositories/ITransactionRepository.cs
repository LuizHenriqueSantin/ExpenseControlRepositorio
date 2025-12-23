using ExpenseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        IQueryable<Transaction> GetAllAsQueryable();
        Task<bool> AddAsync(Transaction obj);
        Task<bool> DeleteAsync(Transaction obj);
        Task<bool> UpdateAsync(Transaction obj);
    }
}
