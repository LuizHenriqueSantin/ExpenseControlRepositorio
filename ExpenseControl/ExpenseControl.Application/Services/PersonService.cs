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
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICurrentUserService _currentUserService;

        public PersonService(IPersonRepository personRepository, ICurrentUserService currentUserService)
        {
            _personRepository = personRepository;
            _currentUserService = currentUserService;
        }      

        public List<PersonOut> GetAll()
        {
            var currentUserId = _currentUserService.GetUserId();

            var query = _personRepository.GetAllAsQueryable()
                .Where(x => x.UserId == currentUserId)
                .Select(x => new PersonOut
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    TotalValueExpense = x.Transactions.Where(x => x.TransactionType == TransactionType.EXPENSE).Sum(x => (double?)x.TransactionValue) ?? 0,
                    TotalValueIncome = x.Transactions.Where(x => x.TransactionType == TransactionType.INCOME).Sum(x => (double?)x.TransactionValue),
                }).ToList();

            return query;
        }

        PersonOut IPersonService.GetById(Guid id)
        {
            var query = _personRepository.GetAllAsQueryable()
                .Where(x => x.Id == id)
                .Select(x => new PersonOut
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    TotalValueExpense = x.Transactions.Where(x => x.TransactionType == TransactionType.EXPENSE).Sum(x => (double?)x.TransactionValue),
                    TotalValueIncome = x.Transactions.Where(x => x.TransactionType == TransactionType.INCOME).Sum(x => (double?)x.TransactionValue),
                }).FirstOrDefault();

            return query ?? new PersonOut();
        }

        List<EntitySelect> IPersonService.GetForSelect()
        {
            var currentUserId = _currentUserService.GetUserId();

            var query = _personRepository.GetAllAsQueryable()
                .Where(x => x.UserId == currentUserId)
                .Select(x => new EntitySelect
                {
                    Label = x.Name,
                    Value = x.Id,
                    Infos = new Infos
                    {
                        Age = x.Age,
                    },
                }).ToList();

            return query;
        }

        public async Task<bool> CreateAsync(PersonIn person)
        {
            if(string.IsNullOrEmpty(person.Name) || person.Age < 0)
            {
                throw new Exception("Impossível criar pessoa");
            }

            var currentUserId = _currentUserService.GetUserId();

            var entity = new Person
            {
                Name = person.Name,
                Age = person.Age,
                UserId = currentUserId,
            };

            return await _personRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = _personRepository.GetAllAsQueryable()
                .FirstOrDefault(x => x.Id == id);

            if(entity == null)
            {
                throw new Exception("Pessoa não encontrada.");
            }

            return await _personRepository.DeleteAsync(entity);
        }

        public async Task<bool> UpdateAsync(PersonIn person)
        {
            var entity = _personRepository.GetAllAsQueryable()
                .FirstOrDefault(x => x.Id == person.Id);

            if(entity == null)
            {
                throw new Exception("Não foi possivel editar pessoa");
            }

            if (string.IsNullOrEmpty(person.Name) || person.Age < 0)
            {
                throw new Exception("Impossível editar pessoa");
            }

            entity.Name = person.Name;
            entity.Age = person.Age;

            return await _personRepository.UpdateAsync(entity);

        }
    }
}
