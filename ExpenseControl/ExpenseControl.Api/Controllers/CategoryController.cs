using ExpenseControl.Application.Interfaces.Services;
using ExpenseControl.Application.Models.In;
using ExpenseControl.Application.Models.Out;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.Api.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<CategoryOut> GetAll()
        {
            return _categoryService.GetAll();
        }

        [HttpGet]
        [Route("GetById")]
        public CategoryOut GetById([FromQuery] Guid id)
        {
            return _categoryService.GetById(id);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CategoryIn category)
        {
            return Ok(await _categoryService.CreateAsync(category));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            return Ok(await _categoryService.DeleteAsync(id));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] CategoryIn category)
        {
            return Ok(await _categoryService.UpdateAsync(category));
        }
    }
}
