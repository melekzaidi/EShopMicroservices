
using Basket.Api.Data;
using Discount.Grpc;
using static Discount.Grpc.DiscountProtoService;

namespace Basket.Ap.Basket.StoreBasket;
public record StockBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandeHandler:AbstractValidator<StockBasketCommand>
{
    public StoreBasketCommandeHandler()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required");

    }

}
public class StockBasketCommandHandler (IBasketRepository repository,DiscountProtoServiceClient discountproto)  : ICommandHandler<StockBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StockBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);
        await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }
    public async Task DeductDiscount(ShoppingCart cart,CancellationToken cancellation)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountproto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
            item.Price -= coupon.Amount;
        }
    }
}
