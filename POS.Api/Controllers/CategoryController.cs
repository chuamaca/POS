using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Interfaces;
using POS.Infraestructure.Commons.Bases.Request;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListCategories([FromBody] BaseFilterRequest filters)
        {
            var response = await _categoryApplication.ListCategories(filters);
            return Ok(response);
        }

        [HttpGet("select")]
        public async Task<IActionResult> ListSelectCategorias()
        {
            var response = _categoryApplication.ListSelectCategories();
            return Ok(response);
        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> CategoryById(int categoryId)
        {
            var response = await _categoryApplication.CategoriesById(categoryId);
            return  Ok(response);
        }

    }
}
