namespace Catalog.Api.Products.DeleteProduct;
    public record DeletePorductResponse(bool IsSuccess);

    public class DeleteProductEndPoint:ICarterModule
    {
    public async void AddRoutes(IEndpointRouteBuilder app)
    {
   app.MapDelete("/products/{id}",async (Guid id,ISender sender) =>
   {
       var result =await sender.Send(new DeleteProductCommand(id));
      // var response=result.Adapt<DeletePorductResponse>();  
       
       return Results.Ok(result);

   }).WithName("DeleteProduct").Produces<DeletePorductResponse>(StatusCodes.Status200OK).ProducesProblem(StatusCodes.Status400BadRequest).ProducesProblem(StatusCodes.Status404NotFound).WithSummary("Delete Product").WithDescription("Delete Product");
    }
    }

