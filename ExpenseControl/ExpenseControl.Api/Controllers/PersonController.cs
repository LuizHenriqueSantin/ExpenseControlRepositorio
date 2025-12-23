using ExpenseControl.Application.Interfaces.Services;
using ExpenseControl.Application.Models.Helpers;
using ExpenseControl.Application.Models.In;
using ExpenseControl.Application.Models.Out;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.Api.Controllers
{
    [Route("api/Person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<PersonOut> GetAll()
        {
            return _personService.GetAll();
        }

        [HttpGet]
        [Route("GetById")]
        public PersonOut GetById([FromQuery] Guid id)
        {
            return _personService.GetById(id);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] PersonIn person)
        {
            return Ok(await _personService.CreateAsync(person));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            return Ok(await _personService.DeleteAsync(id));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] PersonIn person)
        {
            return Ok(await _personService.UpdateAsync(person));
        }
    }
}
