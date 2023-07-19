using BookCatalogAPI.Data;
using BookCatalogAPI.Repositories.Contracts;
using BookCatalogAPI.Entities;
using BookCatalogAPI.Classes;
using BookCatalogModels.Dtos;
using Microsoft.EntityFrameworkCore;
using BookCatalogAPI.Extensions;

namespace BookCatalogAPI.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookDbContext _db;

    public AuthorRepository(BookDbContext db)
    {
        _db = db;
    }

    // Returns pageSize + 1 authors when possible
    public async Task<PagedResponse<Author>> GetAuthorsAsync(AuthorGetQueryParams queryParams)
    {
        var query = _db.Authors.AsQueryable();

        if (queryParams.Name != null)
            query = query.Where(author => author.Name.Contains(queryParams.Name));

        if (queryParams.FromBirth != null && queryParams.ToBirth != null)
        {
            query = query.Where(author => author.DateOfBirth >= queryParams.FromBirth
                                && author.DateOfBirth <= queryParams.ToBirth);
        }
        else if (queryParams.FromBirth != null)
        {
            query = query.Where(author => author.DateOfBirth >= queryParams.FromBirth);
        }

        int totalItems = query.Count();
        var authors = await query
                        .Skip((queryParams.Page - 1) * queryParams.PageSize)
                        .Take(queryParams.PageSize + 1)
                        .ToListAsync();

        return new PagedResponse<Author>()
        {
            Data = authors,
            Page = queryParams.Page,
            PageSize = authors.Count < queryParams.PageSize ? authors.Count : queryParams.PageSize,
            TotalItems = totalItems
        };
    }

    public async Task<Author?> GetAuthorAsync(int id)
    {
        return await _db.Authors.SingleOrDefaultAsync(author => author.Id == id);
    }

    public async Task<Author> CreateAuthorAsync(AuthorCreateDto dto)
    {
        var author = new Author()
        {
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth
        };
        await _db.Authors.AddAsync(author);
        await _db.SaveChangesAsync();
        return author;
    }

    public async Task<Author?> UpdateAuthorAsync(int id, AuthorCreateDto dto)
    {
        var author = await _db.Authors.SingleOrDefaultAsync(author => author.Id == id);
        if (author == null)
            return null;

        author.Name = dto.Name;
        author.DateOfBirth = dto.DateOfBirth;
        await _db.SaveChangesAsync();
        return author;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await _db.Authors.SingleOrDefaultAsync(author => author.Id == id);
        if (author == null)
            return false;

        _db.Authors.Remove(author);
        await _db.SaveChangesAsync();
        return true;
    }
}