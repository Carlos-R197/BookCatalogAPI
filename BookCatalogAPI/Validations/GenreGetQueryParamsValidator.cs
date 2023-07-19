using BookCatalogModels.Dtos;

using FluentValidation;

namespace BookCatalogAPI.Validations;

public class GenreGetQueryParamsValidator : AbstractValidator<GenreGetQueryParams>
{
    public GenreGetQueryParamsValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.Name))
            .WithMessage("name can't exceed 50 characters");
    }
}