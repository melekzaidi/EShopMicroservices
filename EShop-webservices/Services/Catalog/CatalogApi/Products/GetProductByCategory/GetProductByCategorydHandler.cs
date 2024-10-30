using Catalog.Api.Exception;
using Catalog.Api.Products.GetProductByIdHandler;

namespace Catalog.Api.Products.GetProductByCategory;
public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Product);

internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Quey}", query);
        var products = await session.Query<Product>().Where(p=>p.Category.Contains(query.Category)).ToListAsync() ;
      

        return new GetProductByCategoryResult(products);
    }

}
