using ExpenseControl.Application.Interfaces.Repositories;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Transaction> GetAllAsQueryable()
        {
            return _context.Transactions;
        }

        public async Task<bool> AddAsync(Transaction obj)
        {
            try
            {
                _context.Transactions.Add(obj);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao criar transação: {ex}");
            }
        }

        public async Task<bool> DeleteAsync(Transaction obj)
        {
            try
            {
                _context.Transactions.Remove(obj);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao deletar transação: {ex}");
            }
        }

        public async Task<bool> UpdateAsync(Transaction obj)
        {
            try
            {
                _context.Transactions.Update(obj);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao atualizar transação: {ex}");
            }
        }
    }
}
