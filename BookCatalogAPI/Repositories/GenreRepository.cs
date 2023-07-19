using BookCatalogAPI.Entities;
using BookCatalogAPI.Repositories.Contracts;
using BookCatalogAPI.Data;
using BookCatalogModels.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookCatalogAPI.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly BookDbContext _db;

    public GenreRepository(BookDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync(GenreGetQueryParams queryParams)
    {
        var query = _db.Genres.AsQueryable();
        if (queryParams.Name != null)
            query = query.Where(genre => genre.Name.Contains(queryParams.Name));

        return await query.ToListAsync();
    }

    public async Task<Genre?> GetGenreAsync(int id)
    {
        return await _db.Genres.SingleOrDefaultAsync(genre => genre.Id == id);
    }

    public async Task<Genre> CreateGenreAsync(GenreCreateDto dto)
    {
        var genre = new Genre()
        {
            Name = dto.Name,
            Description = dto.Description
        };
        await _db.Genres.AddAsync(genre);
        await _db.SaveChangesAsync();
        return genre;
    }

    public async Task<Genre> UpdateGenreAsync(int id, GenreCreateDto dto)
    {
        var genre = await _db.Genres.SingleAsync(genre => genre.Id == id);
        genre.Name = dto.Name;
        genre.Description = dto.Description;
        _db.Genres.Update(genre);
        await _db.SaveChangesAsync();
        return genre;
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        var genre = await _db.Genres.SingleOrDefaultAsync(genre => genre.Id == id);
        if (genre == null)
            return false;

        _db.Genres.Remove(genre);
        await _db.SaveChangesAsync();
        return true;
    }
}