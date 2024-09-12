using FluentValidation;

namespace MovieApp.Business.DTOs.MovieDTOs;

public record MovieCreateDto(string Title,string Description,bool Isdeleted, int GenreId);

public class MovieCreateDtoValidator : AbstractValidator<MovieCreateDto>
{
    public MovieCreateDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Can not be empty!")
            .NotNull().WithMessage("Can not be null")
            .MinimumLength(1).WithMessage("Minimum length must be 1")
            .MaximumLength(200).WithMessage("Maximum length must be 200");
        
        RuleFor(x => x.Description)
            .NotNull().When(x => !x.Isdeleted).WithMessage("If movie is active description can not be null!")
            .MaximumLength(800).WithMessage("Maximum length must be 800");
       
        RuleFor(x => x.Isdeleted).NotNull();

        RuleFor(x => x.GenreId).NotNull().NotEmpty();

    }

}