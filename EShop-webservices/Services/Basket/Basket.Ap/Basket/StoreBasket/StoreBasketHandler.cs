
using Basket.Api.Data;

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
public class StockBasketCommandHandler (IBasketRepository repository): ICommandHandler<StockBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StockBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }
}
