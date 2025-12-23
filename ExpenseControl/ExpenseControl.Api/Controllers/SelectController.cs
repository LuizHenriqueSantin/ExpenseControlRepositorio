using ExpenseControl.Application.Helpers;
using ExpenseControl.Application.Interfaces.Services;
using ExpenseControl.Application.Models.Helpers;
using ExpenseControl.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.Api.Controllers
{
    [Route("api/Select")]
    [ApiController]
    public class SelectController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;
        public SelectController(IPersonService personService, ICategoryService categoryService)
        {
            _personService = personService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetPurposes")]
        public List<EnumSelect> GetPurposes()
        {
            return Enum.GetValues(typeof(Purpose))
                .Cast<Purpose>()
                .Select(x => new EnumSelect
                {
                    Label = x.GetDisplayName(),
                    Value = (int)x,
                }).ToList();
        }

        [HttpGet]
        [Route("GetTransactionTypes")]
        public List<EnumSelect> GetTransactionTypes()
        {
            return Enum.GetValues(typeof(TransactionType))
                .Cast<TransactionType>()
                .Select(x => new EnumSelect
                {
                    Label = x.GetDisplayName(),
                    Value = (int)x,
                }).ToList();
        }

        [HttpGet]
        [Route("GetPersons")]
        public List<EntitySelect> GetPersons()
        {
            return _personService.GetForSelect();
        }

        [HttpGet]
        [Route("GetCategories")]
        public List<EntitySelect> GetCategories([FromQuery] TransactionType type)
        {
            return _categoryService.GetForSelect(type);
        }
    }
}
