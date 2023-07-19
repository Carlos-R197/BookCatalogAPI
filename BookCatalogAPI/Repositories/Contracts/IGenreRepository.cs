using BookCatalogAPI.Entities;
using BookCatalogModels.Dtos;

namespace BookCatalogAPI.Repositories.Contracts;

public interface IGenreRepository
{
    public Task<IEnumerable<Genre>> GetAllGenresAsync(GenreGetQueryParams queryParams);
    public Task<Genre?> GetGenreAsync(int id);
    public Task<Genre> CreateGenreAsync(GenreCreateDto dto);
    public Task<Genre> UpdateGenreAsync(int id, GenreCreateDto dto);
    public Task<bool> DeleteGenreAsync(int id);
}