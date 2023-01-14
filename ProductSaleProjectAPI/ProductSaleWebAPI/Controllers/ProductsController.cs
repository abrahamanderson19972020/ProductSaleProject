using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductSaleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getall")]
        public IActionResult Get()
        {          
            var result = _productService.GetAllProducts();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetProductById(int productId)
        {
            var result = _productService.GetProductById(productId);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [SecuredOperation("product.add,admin,user")]
        [HttpPost("add")]
        public IActionResult AddProduct(Product product)
        {
            var result = _productService.Add(product);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
