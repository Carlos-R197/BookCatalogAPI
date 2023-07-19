using BookCatalogModels.Dtos;

using FluentValidation;

namespace BookCatalogAPI.Validations;

public class AuthorCreateDtoValidator : AbstractValidator<AuthorCreateDto>
{
    public AuthorCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 50);

        RuleFor(x => x.DateOfBirth)
            .NotEqual(default(DateOnly))
            .LessThan(DateOnly.FromDateTime(DateTime.Now));
    }
}