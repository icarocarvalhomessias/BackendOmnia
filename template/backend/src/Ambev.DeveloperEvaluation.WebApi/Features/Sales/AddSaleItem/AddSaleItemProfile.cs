using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddSaleItem;

/// <summary>
/// Profile for mapping between Application and API AddSaletem responses
/// </summary>
public class AddSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for AddSaletem feature
    /// </summary>
    public AddSaleItemProfile()
    {
        CreateMap<AddSaleItemRequest, UpdateSaleCommand>();
        CreateMap<UpdateSaleResult, AddSaleItemResponse>();
    }
}
