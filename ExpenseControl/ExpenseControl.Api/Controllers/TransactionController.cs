using ExpenseControl.Application.Interfaces.Services;
using ExpenseControl.Application.Models.Out;
using ExpenseControl.Application.Models.In;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.Api.Controllers
{
    [Route("api/Transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<TransactionOut> GetAll()
        {
            return _transactionService.GetAll();
        }

        [HttpGet]
        [Route("GetById")]
        public TransactionOut GetById([FromQuery] Guid id)
        {
            return _transactionService.GetById(id);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] TransactionIn transaction)
        {
            return Ok(await _transactionService.CreateAsync(transaction));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            return Ok(await _transactionService.DeleteAsync(id));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] TransactionIn transaction)
        {
            return Ok(await _transactionService.UpdateAsync(transaction));
        }
    }
}
