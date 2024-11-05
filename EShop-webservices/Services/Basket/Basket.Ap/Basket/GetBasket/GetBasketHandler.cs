using Basket.Api.Data;

namespace Basket.Ap.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IbasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellation)
    {
        // Replace "swn" with the appropriate UserName if needed
        var basket = await repository.GetBasket(query.UserName,cancellation);
        return new GetBasketResult(basket);
    }
}
