using App.Application.Commands.Product;
using App.Application.Queries;
using App.Domain.DTOs.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Controllers
{
    [Authorize]
    [Route("api/products")]
    [ApiController]
    public class ProductController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await sender.Send(new CreateProductCommand(request));
            if (!result.IsSuccessful) return NotFound(result);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await sender.Send(new GetAllProductsQuery(pageSize, pageNumber));
            if (!result.IsSuccessful) return NotFound(result);
            return Ok(result);
        }


        [HttpGet("filter")]
        public async Task<IActionResult> ProductFilterAsync([FromQuery] ProductFilterDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await sender.Send(new ProductFilterQuery(request));
            if (!result.IsSuccessful) return NotFound(result);
            return Ok(result);
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid productId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await sender.Send(new GetProductByIdQuery(productId));
            if (!result.IsSuccessful) return NotFound(result);
            return Ok(result);
        }


        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid productId, [FromBody] UpdateProductDto updateRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await sender.Send(new UpdateProductCommand(productId, updateRequest));
            if (!result.IsSuccessful) return NotFound(result);
            return Ok(result);
        }

        
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid productId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await sender.Send(new DeleteProductCommand(productId));
            if (!result.IsSuccessful) return NotFound(result);
            return Ok(result);
        }
    }
}
