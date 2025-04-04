using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddSaleItem;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sale operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SaleController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a paginated list of sales
    /// </summary>
    /// <param name="_page">The page number</param>
    /// <param name="_size">The number of items per page</param>
    /// <param name="_order">The ordering string (e.g., "id desc")</param>
    /// <returns>A paginated list of sales</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetSalesResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSales([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string _order = "id desc")
    {
        if (_page < 1) _page = 1;
        if (_size < 1) _size = 10;

        var request = new GetSalesQuery(_page, _size, _order);
        var sales = await _mediator.Send(request);

        if (sales is null)
        {
            return NotFound("No sales found");
        }

        return Ok(sales);
    }

    /// <summary>
    /// Adds a new sale item
    /// </summary>
    /// <param name="saleItem">The sale item to add</param>
    /// <returns>The created sale item details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSaleItem([FromBody] AddSaleItemRequest saleItem)
    {
        var command = new CreateSaleCommand
        {
            Branch = saleItem.Branch,
            ProductId = saleItem.ProductId,
            Quantity = saleItem.Quantity,
            CustomerId = GetCurrentUserId()
        };

        var cart = await _mediator.Send(command);
        return CreatedAtAction(nameof(AddSaleItem), new { id = cart.SaleId }, new ApiResponseWithData<CreateSaleResult>
        {
            Success = true,
            Message = "Product added to cart successfully",
            Data = cart
        });
    }
}
