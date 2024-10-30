﻿namespace Catalog.Api.Products.GetProducts;
    public record GetProductsQuery():IQuery<GetProductsResult>;
public record  GetProductsResult (IEnumerable<Product> prodcuts);

    internal class GetProductsQueryHandler(IDocumentSession session,ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery,GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query,CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called wih {@Query}", query);
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }

}
    

