using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetSaleById;

public class GetSaleByIdProfile : Profile
{
    public GetSaleByIdProfile()
    {
        CreateMap<GetSaleByIdRequest, GetSaleByIdHandler>();
        CreateMap<GetSaleByIdHandler, GetSalesResponse>();
    }
}
