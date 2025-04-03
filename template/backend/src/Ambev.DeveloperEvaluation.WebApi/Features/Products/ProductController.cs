using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProductRequest)
    {
        var command = new CreateProductCommand
        {
            Title = createProductRequest.Title,
            Description = createProductRequest.Description,
            Price = createProductRequest.Price,
            Category = createProductRequest.Category,
            Rating = createProductRequest.Rating,
            Count = createProductRequest.Count
        };

        var result = await _mediator.Send(command);
        var response = _mapper.Map<CreateProductResponse>(result);

        return CreatedAtAction(nameof(CreateProduct), new { id = response.Id }, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = response
        });
    }

}