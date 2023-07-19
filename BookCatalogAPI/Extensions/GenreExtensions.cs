using BookCatalogAPI.Entities;
using BookCatalogModels.Dtos;

namespace BookCatalogAPI.Extensions;

public static class GenreExtensions
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description
        };
    }
}
