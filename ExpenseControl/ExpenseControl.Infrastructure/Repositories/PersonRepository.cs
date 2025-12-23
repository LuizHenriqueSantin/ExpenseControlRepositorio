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
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Person> GetAllAsQueryable()
        {
            return _context.Persons;
        }

        public async Task<bool> AddAsync(Person obj)
        {
            try
            {
                _context.Persons.Add(obj);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao criar pessoa: {ex}");
            }
        }

        public async Task<bool> DeleteAsync(Person obj)
        {
            try
            {
                _context.Persons.Remove(obj);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao deletar pessoa: {ex}");
            }
        }

        public async Task<bool> UpdateAsync(Person obj)
        {
            try
            {
                _context.Persons.Update(obj);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao atualizar pessoa: {ex}");
            }
        }
    }
}
