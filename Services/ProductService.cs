using Grpc.Core;
using GrpcProductServer;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductService :ServeProduct.ServeProductBase
{
    private static readonly List<ProductResponse> Products = new();

    public override Task<ProductResponse> GetProduct(ProductRequest request, ServerCallContext context)
    {
        var product = Products.Find(p => p.Id == request.Id);
        return Task.FromResult(product ?? new ProductResponse());
    }

    public override Task<AddProductResponse> AddProduct(AddProductRequest request, ServerCallContext context)
    {
        var newProduct = new ProductResponse
        {
            Id = Products.Count + 1,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        Products.Add(newProduct);

        return Task.FromResult(new AddProductResponse
        {
            Id = newProduct.Id,
            Success = true
        });
    }
}
