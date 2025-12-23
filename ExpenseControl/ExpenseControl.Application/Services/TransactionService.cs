using ExpenseControl.Application.Interfaces.Repositories;
using ExpenseControl.Application.Interfaces.Services;
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
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPersonService _personService;
        private readonly ICurrentUserService _currentUserService;


        public TransactionService(ITransactionRepository transactionRepository, IPersonService personService, ICurrentUserService currentUserService)
        {
            _transactionRepository = transactionRepository;
            _personService = personService;
            _currentUserService = currentUserService;
        }

        public List<TransactionOut> GetAll()
        {
            var currentUserId = _currentUserService.GetUserId();

            var query = _transactionRepository.GetAllAsQueryable()
                .Where(x => x.Person.UserId == currentUserId)
                .Select(x => new TransactionOut
                {
                    Id = x.Id,
                    TransactionDescription = x.TransactionDescription,
                    TransactionType = x.TransactionType,
                    TransactionValue = x.TransactionValue,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.CategoryDescription,
                    PersonId = x.PersonId,
                    PersonName = x.Person.Name,
                }).ToList();

            return query;
        }

        public TransactionOut GetById(Guid id)
        {
            var query = _transactionRepository.GetAllAsQueryable()
                .Where(x => x.Id == id)
                .Select(x => new TransactionOut
                {
                    Id = x.Id,
                    TransactionDescription = x.TransactionDescription,
                    TransactionType = x.TransactionType,
                    TransactionValue = x.TransactionValue,
                    CategoryId = x.CategoryId,
                    PersonId = x.PersonId,
                }).FirstOrDefault();

            return query ?? new TransactionOut();
        }

        public async Task<bool> CreateAsync(TransactionIn transaction)
        {
            if (transaction == null ||
                transaction.TransactionValue == 0 ||
                transaction.TransactionType == 0 ||
                string.IsNullOrEmpty(transaction.TransactionDescription) ||
                transaction.CategoryId == Guid.Empty ||
                transaction.PersonId == Guid.Empty) throw new Exception("Não foi possível criar transação");

            if(transaction.TransactionType == TransactionType.INCOME)
            {
                var personAge = _personService.GetById(transaction.PersonId).Age;

                if (personAge < 18) throw new Exception("Transação não autorizada");
            }

            var entity = new Transaction
            {
                TransactionDescription = transaction.TransactionDescription,
                TransactionType = transaction.TransactionType,
                TransactionValue = transaction.TransactionValue,
                CategoryId = transaction.CategoryId,
                PersonId = transaction.PersonId,
            };

            return await _transactionRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = _transactionRepository.GetAllAsQueryable()
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception("Transação não encontrada.");
            }

            return await _transactionRepository.DeleteAsync(entity);
        }

        public async Task<bool> UpdateAsync(TransactionIn transaction)
        {
            if (transaction == null ||
                transaction.TransactionValue == 0 ||
                transaction.TransactionType == 0 ||
                string.IsNullOrEmpty(transaction.TransactionDescription) ||
                transaction.CategoryId == Guid.Empty ||
                transaction.PersonId == Guid.Empty) throw new Exception("Não foi possível editar transação");

            var entity = _transactionRepository.GetAllAsQueryable()
                .FirstOrDefault(x => x.Id == transaction.Id);

            if (entity == null)
            {
                throw new Exception("Não foi possivel editar transação");
            }

            if(entity.PersonId != transaction.PersonId && entity.TransactionType == TransactionType.INCOME)
            {
                var personAge = _personService.GetById(transaction.PersonId).Age;

                if (personAge < 18) throw new Exception("Transação não autorizada");
            }

            entity.TransactionDescription = transaction.TransactionDescription;
            entity.TransactionType = transaction.TransactionType;
            entity.TransactionValue = transaction.TransactionValue;
            entity.CategoryId = transaction.CategoryId;
            entity.PersonId = transaction.PersonId;

            return await _transactionRepository.UpdateAsync(entity);
        }
    }
}
