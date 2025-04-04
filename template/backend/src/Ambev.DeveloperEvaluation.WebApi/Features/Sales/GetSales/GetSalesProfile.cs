using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetSales;

public class GetSalesProfile : Profile
{
    public GetSalesProfile()
    {
        CreateMap< GetSalesRequest, GetSalesHandler>();
        CreateMap<GetSalesHandler, IList<GetSalesResponse>>();
    }
}
