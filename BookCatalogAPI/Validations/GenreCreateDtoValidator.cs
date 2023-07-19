using FluentValidation;
using BookCatalogModels.Dtos;

namespace BookCatalogAPI.Validations;

public class GenreCreateDtoValidator : AbstractValidator<GenreCreateDto>
{
    public GenreCreateDtoValidator()
    {
        RuleFor(x => x.Name).Length(3, 50);
        RuleFor(x => x.Description).Length(10, 250);
    }
}