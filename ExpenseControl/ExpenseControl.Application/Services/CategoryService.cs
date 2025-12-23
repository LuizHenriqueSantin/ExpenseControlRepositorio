using ExpenseControl.Application.Helpers;
using ExpenseControl.Application.Interfaces.Repositories;
using ExpenseControl.Application.Interfaces.Services;
using ExpenseControl.Application.Models.Helpers;
using ExpenseControl.Application.Models.In;
using ExpenseControl.Application.Models.Out;
using ExpenseControl.Domain.Entities;
using ExpenseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;

        public CategoryService(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public List<CategoryOut> GetAll()
        {
            var currentUserId = _currentUserService.GetUserId();

            var query = _categoryRepository.GetAllAsQueryable()
                .Where(x => x.UserId == currentUserId)
                .Select(x => new CategoryOut
                {
                    Id = x.Id,
                    CategoryDescription = x.CategoryDescription,
                    Purpose = x.Purpose,
                    PurposeName = x.Purpose.GetDisplayName(),
                    TotalValueExpense = x.Transactions.Where(x => x.TransactionType == TransactionType.EXPENSE).Sum(x => (double?)x.TransactionValue),
                    TotalValueIncome = x.Transactions.Where(x => x.TransactionType == TransactionType.INCOME).Sum(x => (double?)x.TransactionValue),
                }).ToList();

            return query;
        }

        public CategoryOut GetById(Guid id)
        {
            var query = _categoryRepository.GetAllAsQueryable()
                .Where(x => x.Id == id)
                .Select(x => new CategoryOut
                {
                    Id = x.Id,
                    CategoryDescription = x.CategoryDescription,
                    Purpose = x.Purpose,
                    PurposeName = x.Purpose.GetDisplayName(),
                    TotalValueExpense = x.Transactions.Where(x => x.TransactionType == TransactionType.EXPENSE).Sum(x => (double?)x.TransactionValue),
                    TotalValueIncome = x.Transactions.Where(x => x.TransactionType == TransactionType.INCOME).Sum(x => (double?)x.TransactionValue),
                }).FirstOrDefault();

            return query ?? new CategoryOut();
        }

        public List<EntitySelect> GetForSelect(TransactionType type)
        {
            var currentUserId = _currentUserService.GetUserId();

            var query = _categoryRepository.GetAllAsQueryable()
                .Where(x => x.UserId == currentUserId);

            switch(type)
            {
                case TransactionType.EXPENSE: query = query.Where(x => x.Purpose == Purpose.EXPENSE);
                    break;

                case TransactionType.INCOME: query = query.Where(x => x.Purpose == Purpose.INCOME);
                    break;
                default:
                    break;
            }

            var list = query.Select(x => new EntitySelect
                {
                    Label = x.CategoryDescription,
                    Value = x.Id,
                }).ToList();

            return list;
        }
        public async Task<bool> CreateAsync(CategoryIn category)
        {
            if(string.IsNullOrEmpty(category.CategoryDescription) || category.Purpose == 0)
            {
                throw new Exception("Não foi possível criar categoria");
            }

            var currentUserId = _currentUserService.GetUserId();

            var entity = new Category
            {
                CategoryDescription = category.CategoryDescription,
                Purpose = category.Purpose,
                UserId = currentUserId,
            };

            return await _categoryRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = _categoryRepository.GetAllAsQueryable()
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception("Categoria não encontrada.");
            }

            return await _categoryRepository.DeleteAsync(entity);
        }

        public async Task<bool> UpdateAsync(CategoryIn category)
        {
            if (string.IsNullOrEmpty(category.CategoryDescription) || category.Purpose == 0)
            {
                throw new Exception("Impossível editar categoria");
            }

            var entity = _categoryRepository.GetAllAsQueryable()
                .FirstOrDefault(x => x.Id == category.Id);

            if (entity == null)
            {
                throw new Exception("Não foi possivel editar categoria");
            }

            entity.CategoryDescription = category.CategoryDescription;
            entity.Purpose = category.Purpose;

            return await _categoryRepository.UpdateAsync(entity);
        }
    }
}
