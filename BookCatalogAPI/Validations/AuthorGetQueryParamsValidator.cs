using BookCatalogModels.Dtos;
using FluentValidation;

namespace BookCatalogAPI.Validations;

public class AuthorGetQueryParamsValidator : AbstractValidator<AuthorGetQueryParams>
{
    public AuthorGetQueryParamsValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.Name))
            .WithMessage("name can't exceed 50 characters");

        RuleFor(x => x.FromBirth)
            .LessThan(x => x.ToBirth)
            .When(x => x.FromBirth != null && x.ToBirth != null)
            .WithMessage("Must follow the YYYY-MM-DD format. Also," +
                " fromBirth must be less than toBirth.");

        RuleFor(x => x.ToBirth)
            .GreaterThan(x => x.FromBirth)
            .When(x => x.ToBirth != null)
            .WithMessage("Must follow the YYYY-MM-DD format. Also," +
                " toBirth may only be present with fromBirth and must be greater than fromBirth");

        RuleFor(x => x.Page)
            .GreaterThan(0)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}