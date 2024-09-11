using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Core.Entities;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Interfaces;

public interface IMovieService
{
    Task<MovieGetDto> CreateAsync(MovieCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int? id, MovieUpdateDto dto);
    Task<MovieGetDto> GetByIdAsync(int id);
    Task<ICollection<MovieGetDto>> GetByExpressionAsync(Expression<Func<Movie, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
    Task<MovieGetDto> GetSingleByExpressionAsync(Expression<Func<Movie, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
}
