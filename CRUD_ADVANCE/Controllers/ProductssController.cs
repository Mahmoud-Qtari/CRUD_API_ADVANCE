using CRUD_ADVANCE.Data;
using CRUD_ADVANCE.DTOs.ProductDTO;
using CRUD_ADVANCE.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRUD_ADVANCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductssController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<ProductssController> logger;

        public ProductssController(ApplicationDbContext context,ILogger<ProductssController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> getAll()
        {

            var product = await context.productss.Select
            (
                ProductDto => new GetAllProductDTO()
                {
                    Id = ProductDto.Id,
                    Name = ProductDto.Name,
                }
            ).ToListAsync();
            return Ok(product);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> getById(int id)
        {
            var product = await context.productss.FindAsync(id);
            if (product is null)
            {
                return NotFound("product not found");
            }
            DetailsProductDTO productDTO = new DetailsProductDTO()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };
            return Ok(productDTO);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductDTO productaDTO,
            [FromServices]IValidator<CreateProductDTO> validator)
        {
            var validationResult = validator.Validate(productaDTO);
            if (!validationResult.IsValid)
            {
                var modelState = new ModelStateDictionary();
                validationResult.Errors.ForEach(error =>
                {
                    modelState.AddModelError(error.PropertyName,error.ErrorMessage);
                }) ;
                return ValidationProblem(modelState);
            }
            Productss product = new Productss()
            {
                Name = productaDTO.Name,
                Description = productaDTO.Description,
                Price = productaDTO.Price,
            };
            //api controller check modelState automatic
            await context.productss.AddAsync(product);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, UpdateProductDTO productDTO)
        {
            var product = await context.productss.FindAsync(id);
            if (product is null)
            {
                return NotFound("product not found");
            }

            //GetByIdDTO getDepdto = new GetByIdDTO()
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //};

            Productss newProduct = new Productss()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
            };
            product.Name = newProduct.Name;
            product.Description = newProduct.Description;
            product.Price = newProduct.Price;
            await context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await context.productss.FindAsync(id);
            if (product is null)
            {
                return NotFound("product not found");
            }
            context.productss.Remove(product);
            await context.SaveChangesAsync();
            RemoveProductDTO productDTO = new RemoveProductDTO()
            {
                Name = product.Name,
            };
            return Ok(productDTO);
        }


    }
}
