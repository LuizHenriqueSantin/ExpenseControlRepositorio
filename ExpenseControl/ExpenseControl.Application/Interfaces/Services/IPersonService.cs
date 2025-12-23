using ExpenseControl.Application.Models.Helpers;
using ExpenseControl.Application.Models.Out;
using ExpenseControl.Application.Models.In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Interfaces.Services
{
    public interface IPersonService
    {
        List<PersonOut> GetAll();
        PersonOut GetById(Guid id);
        List<EntitySelect> GetForSelect();
        Task<bool> CreateAsync(PersonIn person);
        Task<bool> UpdateAsync(PersonIn person);
        Task<bool> DeleteAsync(Guid id);
    }
}
