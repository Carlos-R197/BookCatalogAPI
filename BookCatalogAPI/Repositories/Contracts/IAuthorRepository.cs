using BookCatalogAPI.Entities;
using BookCatalogAPI.Classes;
using BookCatalogModels.Dtos;

namespace BookCatalogAPI.Repositories.Contracts;

public interface IAuthorRepository
{
    public Task<PagedResponse<Author>> GetAuthorsAsync(AuthorGetQueryParams queryParams);
    public Task<Author?> GetAuthorAsync(int id);
    public Task<Author> CreateAuthorAsync(AuthorCreateDto dto);
    public Task<Author?> UpdateAuthorAsync(int id, AuthorCreateDto dto);
    public Task<bool> DeleteAuthorAsync(int id);
}