using Ambev.DeveloperEvaluation.Application.Sales.CloseSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CloseSale;

public class CloseSaleProfile : Profile
{
    public CloseSaleProfile()
    {
        CreateMap<CloseSaleRequest, CloseSaleCommand>();
        CreateMap<CloseSaleCommand, CloseSaleResponse>();
    }
}
