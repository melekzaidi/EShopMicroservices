
using BuildingBlocks.Exceptions;

namespace Basket.Api.Exception
{

    public class BasketNotFoundException : NotFoundExceptions
    {
        public BasketNotFoundException(string Username) : base("basket not found!", Username) { }

    }
}
