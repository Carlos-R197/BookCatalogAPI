using BookCatalogModels.Dtos;
using BookCatalogAPI.Entities;
using BookCatalogAPI.Classes;

namespace BookCatalogAPI.Extensions;

public static class BookExtensions
{
    public static BookDto ToDto(this Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublicationDate = book.PublicationDate,
            ISBN = book.ISBN
        };
    }

    public static BookDetailsDto ToDetailedDto(this Book book)
    {
        var detailedBook = new BookDetailsDto()
        {
            Id = book.Id,
            Title = book.Title,
            PublicationDate = book.PublicationDate,
            ISBN = book.ISBN,
        };
        foreach (var bookAuthor in book.BookAuthors)
        {
            detailedBook.Authors.Add(bookAuthor.Author.ToDto());
        }
        foreach (var bookGenre in book.BookGenres)
        {
            detailedBook.Genres.Add(bookGenre.Genre.ToDto());
        }
        return detailedBook;
    }

    public static PagedResponse<BookDto> ToDto(this PagedResponse<Book> pagedList)
    {
        return new PagedResponse<BookDto>()
        {
            Data = pagedList.Data.Select(book => book.ToDto()).ToList(),
            Page = pagedList.Page,
            PageSize = pagedList.PageSize,
            TotalItems = pagedList.TotalItems
        };
    }
}