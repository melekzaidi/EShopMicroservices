﻿
namespace Catalog.Api.Products.GetProducts;
public record GetProductsResponse(IEnumerable<Product> products);

public class GetProductsEndPoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
    //       var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(result);
        }).WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
