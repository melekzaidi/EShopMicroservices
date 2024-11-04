
using BuildingBlocks.Exceptions;

namespace Catalog.Api.Exception
{

    public class ProductNotFoundException:NotFoundExceptions 
    {
        public ProductNotFoundException(Guid id):base("Product not found!",id) { }

    }
}
