using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing CreateProductCommand requests
/// </summary>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of CreateProductHandler
    /// </summary>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="productRepository">The product repository</param>
    public CreateProductHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    /// <summary>
    /// Handles the CreateProductCommand request
    /// </summary>
    /// <param name="command">The CreateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = _mapper.Map<Product>(command);

        var rating = new Rating
        {
            Id = Guid.NewGuid(),
            Rate = command.Rating,
            Count = command.Count
        };

        product.Rating = rating;

        var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);

        var result = _mapper.Map<CreateProductResult>(createdProduct);

        return result;
    }
}
