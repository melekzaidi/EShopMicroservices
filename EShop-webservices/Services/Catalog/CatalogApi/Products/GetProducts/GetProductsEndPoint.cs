﻿
namespace Catalog.Api.Products.GetProducts;
public record GetProductsRequest(int? PageNumber=1,int? PageSize=10);

public record GetProductsResponse(IEnumerable<Product> products);

public class GetProductsEndPoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(query);  // Using the query adapted from request

            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
