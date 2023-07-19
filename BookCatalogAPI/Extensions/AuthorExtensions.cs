using BookCatalogModels.Dtos;
using BookCatalogAPI.Entities;
using BookCatalogAPI.Classes;

namespace BookCatalogAPI.Extensions;

public static class AuthorExtensions
{
    public static AuthorDto ToDto(this Author author)
    {
        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };
    }

    public static PagedResponse<AuthorDto> ToDto(this PagedResponse<Author> pagedAuthors)
    {
        return new PagedResponse<AuthorDto>()
        {
            Data = pagedAuthors.Data.Select(author => author.ToDto()).ToList(),
            Page = pagedAuthors.Page,
            PageSize = pagedAuthors.PageSize,
            TotalItems = pagedAuthors.TotalItems
        };
    }
}