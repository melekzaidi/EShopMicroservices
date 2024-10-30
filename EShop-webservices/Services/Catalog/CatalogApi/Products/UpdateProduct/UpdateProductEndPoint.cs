

using Catalog.Api.Products.GetProducts;

namespace Catalog.Api.Products.UpdateProduct;
public record UpdateProductRequest(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record UpdateProductResponse(bool IsSuccsess);



public class UpdateProductEndPoint:ICarterModule
{
   public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products",async (UpdateProductRequest request,ISender sender) =>
        {
            var command=request.Adapt<UpdateProductCommand>();
            var result=await sender.Send(command);
          //  var response=result.Adapt<UpdateProductResponse>();
            return Results.Ok(result);
        }).WithName("UpdateProduct")
        .Produces<UpdateProductRequest>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
