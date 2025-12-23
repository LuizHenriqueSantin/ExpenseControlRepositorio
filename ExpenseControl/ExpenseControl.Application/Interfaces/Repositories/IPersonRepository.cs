using ExpenseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        IQueryable<Person> GetAllAsQueryable();
        Task<bool> AddAsync(Person obj);
        Task<bool> DeleteAsync(Person obj);
        Task<bool> UpdateAsync(Person obj);
    }
}
