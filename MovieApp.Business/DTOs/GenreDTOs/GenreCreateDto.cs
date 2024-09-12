using FluentValidation;

namespace MovieApp.Business.DTOs.GenreDTOs;
public record GenreCreateDto(string Name, bool IsDeleted);

public class GenreCreateDtoValidator: AbstractValidator<GenreCreateDto>
{
    public GenreCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(50).MinimumLength(3);
        RuleFor(x => x.IsDeleted).NotNull();
    }
}

