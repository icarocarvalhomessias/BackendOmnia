using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CloseSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteSale;

public class DeleteSaleProfile : Profile
{
    public DeleteSaleProfile()
    {
        CreateMap<CloseSaleRequest, DeleteSaleCommand>();
        CreateMap<DeleteSaleCommand, DeleteSaleResponse>();
    }

}
