using BookCatalogAPI.Entities;
using BookCatalogAPI.Repositories.Contracts;
using BookCatalogAPI.Data;
using BookCatalogAPI.Classes;
using BookCatalogModels.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookCatalogAPI.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookDbContext _db;

    public BookRepository(BookDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResponse<Book>> GetBooksAsync(BookGetQueryParams queryParams)
    {
        var query = _db.Books.AsQueryable();

        if (queryParams.AuthorId != null)
        {
            query = query.Include(book => book.BookAuthors);
            query = query.Where(book => book.BookAuthors.Any(bookAuthor => bookAuthor.AuthorId == queryParams.AuthorId));
        }

        if (queryParams.GenreId != null)
        {
            query = query.Include(book => book.BookGenres);
            query = query.Where(book => book.BookGenres.Any(bookGenre => bookGenre.GenreId == queryParams.GenreId));
        }

        if (queryParams.Title != null)
            query = query.Where(book => book.Title.Contains(queryParams.Title, StringComparison.CurrentCultureIgnoreCase));

        if (queryParams.FromPublishedDate != null && queryParams.ToPublishedDate != null)
        {
            query = query.Where(book => book.PublicationDate >= queryParams.FromPublishedDate
                                && book.PublicationDate <= queryParams.ToPublishedDate);
        }
        else if (queryParams.FromPublishedDate != null)
        {
            query = query.Where(book => book.PublicationDate >= queryParams.FromPublishedDate);
        }

        var totalItems = query.Count();
        var books = await query
                            .Skip((queryParams.Page - 1) * queryParams.PageSize)
                            .Take(queryParams.PageSize)
                            .ToListAsync();

        return new PagedResponse<Book>()
        {
            Data = books,
            Page = queryParams.Page,
            PageSize = books.Count < queryParams.PageSize ? books.Count : queryParams.PageSize,
            TotalItems = totalItems
        };
    }

    public async Task<Book?> TryGetDetailedBookAsync(int id)
    {
        return await _db.Books
            .Include(book => book.BookAuthors)
                .ThenInclude(bookAuthor => bookAuthor.Author)
            .Include(book => book.BookGenres)
                .ThenInclude(bookGenre => bookGenre.Genre)
            .SingleOrDefaultAsync(book => book.Id == id);
    }

    public async Task<Book> GetDetailedBookAsync(int id)
    {
        return await _db.Books
            .Include(book => book.BookAuthors)
                .ThenInclude(bookAuthor => bookAuthor.Author)
            .Include(book => book.BookGenres)
                .ThenInclude(bookGenre => bookGenre.Genre)
            .SingleAsync(book => book.Id == id);
    }

    public async Task<Book> CreateBookAsync(BookCreateDto dto)
    {
        var book = new Book()
        {
            Title = dto.Title,
            PublicationDate = dto.PublicationDate,
            ISBN = dto.ISBN,
            BookAuthors = new List<BookAuthor>(),
            BookGenres = new List<BookGenre>()
        };
        foreach (int authorId in dto.AuthorIds)
        {
            var bookAuthor = new BookAuthor()
            {
                AuthorId = authorId
            };
            book.BookAuthors.Add(bookAuthor);
        }
        foreach (int genreId in dto.GenreIds)
        {
            var bookGenre = new BookGenre()
            {
                GenreId = genreId
            };
            book.BookGenres.Add(bookGenre);
        }
        await _db.Books.AddAsync(book);
        await _db.SaveChangesAsync();
        return book;
    }

    // Returns null if book doesn't exist
    public async Task<Book?> UpdateBookAsync(int id, BookCreateDto dto)
    {
        var book = await _db.Books
            .Include(book => book.BookAuthors)
            .Include(book => book.BookGenres)
            .SingleOrDefaultAsync(book => book.Id == id);
        if (book == null)
            return null;

        book.Title = dto.Title;
        book.PublicationDate = dto.PublicationDate;
        book.ISBN = dto.ISBN;
        book.BookAuthors.Clear();
        book.BookGenres.Clear();
        foreach (int authorId in dto.AuthorIds)
        {
            var bookAuthor = new BookAuthor()
            {
                BookId = book.Id,
                AuthorId = authorId
            };
            book.BookAuthors.Add(bookAuthor);
        }
        foreach (int genreId in dto.GenreIds)
        {
            var bookGenre = new BookGenre()
            {
                BookId = book.Id,
                GenreId = genreId
            };
            book.BookGenres.Add(bookGenre);
        }

        await _db.SaveChangesAsync();
        return book;
    }

    // Returns false if book doesn't exist
    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _db.Books.SingleOrDefaultAsync(book => book.Id == id);
        if (book == null)
            return false;

        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
        return true;
    }
}