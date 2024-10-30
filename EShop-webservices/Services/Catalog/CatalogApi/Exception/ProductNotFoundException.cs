
namespace Catalog.Api.Exception
{

    public class ProductNotFoundException:SystemException 
    {
        public ProductNotFoundException():base("Product not found!") { }

    }
}
