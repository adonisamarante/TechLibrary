﻿using TechLibrary.Api.Infrastructure.DataAccess;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.UseCases.Books.Filter
{
    public class FilterBookUseCase
    {
        private const int PAGE_SIZE = 10;

        public ResponseBooksJson Execute(RequestFilterBooksJson request)
        {
            var dbContext = new TechLibraryDbContext();

            var skip = (request.PageNumber - 1) * PAGE_SIZE;

            var query = dbContext.Books.AsQueryable();
            if (string.IsNullOrWhiteSpace(request.Title) == false)
            {
                query = query.Where(book => book.Title.Contains(request.Title));
            }

            var books = query
                .OrderBy(book => book.Title).ThenBy(book => book.Author)
                .Skip(skip) // skips the first books based on the PageNumber received
                .Take(PAGE_SIZE) // takes only the amount of books defined by PAGE_SIZE, ignoring the rest of the list
                .ToList(); // toList should always be the last method in the chain, as it executes the query and returns the result

            var totalCount = 0;
            if (string.IsNullOrWhiteSpace(request.Title))
                totalCount = dbContext.Books.Count();
            else
                totalCount = dbContext.Books.Count(book => book.Title.Contains(request.Title));


                return new ResponseBooksJson
                {
                    Pagination = new ResponsePaginationJson
                    {
                        PageNumber = request.PageNumber,
                        TotalCount = totalCount
                    },
                    Books = books.Select(book => new ResponseBookJson
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author
                    }).ToList(),
                };
        }
    }
}
