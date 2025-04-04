using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.EditSale;

public class EditSaleProfile : Profile
{
    public EditSaleProfile()
    {
        CreateMap<EditSaleRequest, UpdateSaleCommand>();
        CreateMap<UpdateSaleCommand, EditSaleResponse>();
    }
}
