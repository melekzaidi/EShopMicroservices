

namespace Basket.API.Basket.DeleteBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellation)
    {
        // Replace "swn" with the appropriate UserName if needed
        return new GetBasketResult(new ShoppingCart(query.UserName));
    }
}
