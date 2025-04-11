using TechLibrary.Api.Infrastructure.DataAccess;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Checkouts
{
    public class RegisterBookCheckoutUseCase
    {
        public void Execute(Guid bookId)
        {
            var dbContext = new TechLibraryDbContext();

            Validate(dbContext, bookId);

            dbContext.Checkouts.Add(new Domain.Entities.Checkout
            {

            });

            dbContext.SaveChanges();
        }

        private void Validate(TechLibraryDbContext dbContext, Guid bookId)
        {
            var book = dbContext.Books.FirstOrDefault(book => book.Id == bookId);

            if (book is null)
            {
                throw new NotFoundException("Livro não encontrado.");
            }

            var amountBookNotReturned = dbContext.Checkouts.Count(checkout => checkout.BookId == bookId && checkout.ReturnedDate == null);

            if (amountBookNotReturned == book.Amount)
            {
                throw new ConflictException("Livro não está disponível para empréstimo");
            }
        }
    }
}
