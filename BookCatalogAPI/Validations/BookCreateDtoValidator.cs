using BookCatalogModels.Dtos;

using FluentValidation;

namespace BookCatalogAPI.Validations;

public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
{
    public BookCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .Length(3, 255);
        RuleFor(x => x.PublicationDate)
            .NotEqual(default(DateOnly))
            .LessThan(DateOnly.FromDateTime(DateTime.Now));
        RuleFor(x => x.ISBN)
            .Length(9, 13);
        RuleFor(x => x.AuthorIds)
            .NotEmpty();
        RuleForEach(x => x.AuthorIds)
            .GreaterThan(0);
        RuleFor(x => x.GenreIds)
            .NotEmpty();
        RuleForEach(x => x.GenreIds)
            .GreaterThan(0);
    }
}