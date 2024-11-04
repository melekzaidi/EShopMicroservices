using Catalog.Api.Exception;
using Catalog.Api.Products.CreateProduct;
using Catalog.Api.Products.GetProducts;
using FluentValidation;

namespace Catalog.Api.Products.UpdateProduct;
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    :ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(commande => commande.Id).NotEmpty().WithMessage("Prodcut Id is Required");
        RuleFor(commande => commande.Name).NotEmpty().WithMessage("Name is Required").Length(2, 150).WithMessage("Name must between 2 and 50 characters");
        RuleFor(commande => commande.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class updateProductsCommandHandler(IDocumentSession session, ILogger<updateProductsCommandHandler> logger, IValidator<UpdateProductCommand> validator) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(command, cancellationToken);
        var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors.FirstOrDefault());
        }
        logger.LogInformation("UpdateProductsHandlerHandler.Handle called wih {@Command}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null) {
            throw new ProductNotFoundException(command.Id);
        }
        product.Name=command.Name;
        product.Category=command.Category;  
        product.Description=command.Description;    
        product.ImageFile=command.ImageFile;    
        product.Price=command.Price;    
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
            
    }

}

