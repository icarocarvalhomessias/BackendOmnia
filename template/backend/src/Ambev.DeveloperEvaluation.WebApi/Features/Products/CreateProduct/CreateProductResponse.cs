namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductResponse
    {
        /// <summary>
        /// The unique identifier of the created product
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The product title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The product price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The product description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The product category
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// The product image URL
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// The product rating
        /// </summary>
        public RatingCreate Rating { get; set; } = new RatingCreate();

        /// <summary>
        /// Represents the rating of a product.
        /// Includes the rate and count of ratings.
        /// </summary>
        public class RatingCreate
        {
            /// <summary>
            /// The rate of the product
            /// </summary>
            public decimal Rate { get; set; }

            /// <summary>
            /// The count of ratings
            /// </summary>
            public int Count { get; set; }
        }
    }
}
