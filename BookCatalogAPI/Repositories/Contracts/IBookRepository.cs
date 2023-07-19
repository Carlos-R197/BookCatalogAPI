using BookCatalogAPI.Entities;
using BookCatalogAPI.Classes;
using BookCatalogModels.Dtos;

namespace BookCatalogAPI.Repositories.Contracts;

public interface IBookRepository
{
    public Task<PagedResponse<Book>> GetBooksAsync(BookGetQueryParams queryParams);
    public Task<Book?> TryGetDetailedBookAsync(int id);
    public Task<Book> GetDetailedBookAsync(int id);
    public Task<Book> CreateBookAsync(BookCreateDto bookCreateDto);
    public Task<Book?> UpdateBookAsync(int id, BookCreateDto bookCreateDto);
    public Task<bool> DeleteBookAsync(int id);
}