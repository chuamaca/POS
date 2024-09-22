using Microsoft.AspNetCore.Mvc;
using POS.Application.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;
        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }
    }
}
