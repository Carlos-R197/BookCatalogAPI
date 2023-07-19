using BookCatalogModels.Dtos;
using FluentValidation;

namespace BookCatalogAPI.Validations;

public class BookGetQueryParamsValidator : AbstractValidator<BookGetQueryParams>
{
    public BookGetQueryParamsValidator()
    {
        RuleFor(x => x.Title)
            .Length(3, 255)
            .When(x => x.Title != null)
            .WithMessage("title can't exceed 255 characters");

        RuleFor(x => x.FromPublishedDate)
            .LessThan(x => x.ToPublishedDate)
            .When(x => x.FromPublishedDate != null && x.ToPublishedDate != null)
            .WithMessage("fromPublishedDate must be a date in the format YYYY-MM-DD and be less than toPublishedDate");

        RuleFor(x => x.ToPublishedDate)
            .GreaterThan(x => x.FromPublishedDate)
            .When(x => x.ToPublishedDate != null)
            .WithMessage("toPublishedDate must be a date in the format YYYY-MM-DD and be greater than fromPublishedDate");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue)
            .When(x => x.AuthorId != null)
            .WithMessage("authorId must be bigger than 0 and fit into a int32");

        RuleFor(x => x.GenreId)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue)
            .When(x => x.GenreId != null)
            .WithMessage("genreId must be bigger than 0 and fit into a int32");

        RuleFor(x => x.Page)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}