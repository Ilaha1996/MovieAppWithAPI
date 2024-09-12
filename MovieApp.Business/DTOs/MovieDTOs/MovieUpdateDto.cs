﻿using FluentValidation;

namespace MovieApp.Business.DTOs.MovieDTOs;
public record MovieUpdateDto(string Title, string Description,bool IsDeleted, int GenreId/*double CostPrice, double SalePrice*/);

public class MovieUpdateDtoValidator:AbstractValidator<MovieUpdateDto>
{
    public MovieUpdateDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Can not be empty!")
            .NotNull().WithMessage("Can not be null")
            .MinimumLength(1).WithMessage("Minimum length must be 1")
            .MaximumLength(200).WithMessage("Maximum length must be 200");

        RuleFor(x => x.Description)
            .NotNull().When(x => !x.IsDeleted).WithMessage("If movie is active description can not be null!")
            .MaximumLength(800).WithMessage("Maximum length must be 800");

        RuleFor(x => x.IsDeleted).NotNull();

        RuleFor(x => x.GenreId).NotNull().NotEmpty();

        //RuleFor(x => x).Custom((x, context) =>
        //{
        //    if (x.SalePrice < x.CostPrice)
        //    {
        //        context.AddFailure("SalePrice", "SalePrice must be greater than CostPrice");
        //    }
        //});
    }
}