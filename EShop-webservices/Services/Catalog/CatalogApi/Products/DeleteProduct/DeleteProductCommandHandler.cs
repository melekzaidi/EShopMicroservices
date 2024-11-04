using Catalog.Api.Products.CreateProduct;
using System.Threading;

namespace Catalog.Api.Products.DeleteProduct;
    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("id is required");

    }
}

internal class DeleteProductCommandHandler(IDocumentSession session, IValidator<DeleteProductCommand> validator) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command,CancellationToken cancellationtoken)
    {
        var result = await validator.ValidateAsync(command, cancellationtoken);
        var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors.FirstOrDefault());
        }
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationtoken);
        return new DeleteProductResult(true);


    }

}
    

