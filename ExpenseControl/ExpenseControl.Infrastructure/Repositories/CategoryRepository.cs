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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAllAsQueryable()
        {
            return _context.Categories;
        }

        public async Task<bool> AddAsync(Category obj)
        {
            try
            {
                _context.Categories.Add(obj);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao criar categoria: {ex}");
            }
        }

        public async Task<bool> DeleteAsync(Category obj)
        {
            try
            {
                _context.Categories.Remove(obj);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao deletar categoria: {ex}");
            }
        }

        public async Task<bool> UpdateAsync(Category obj)
        {
            try
            {
                _context.Categories.Update(obj);
                await _context.SaveChangesAsync();

                return true;
            } 
            catch (Exception ex)
            {
                throw new Exception($"Falha ao atualizar categoria: {ex}");
            }
        }
    }
}
