using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Core.Entities;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Interfaces;

public interface IGenreService
{
    Task CreateAsync(GenreCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int? id, GenreUpdateDto dto);
    Task<GenreGetDto> GetByIdAsync(int id);
    Task<bool> IsExistAsync(Expression<Func<Genre, bool>>? expression = null);
    Task<ICollection<GenreGetDto>> GetByExpressionAsync(Expression<Func<Genre, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
    Task<GenreGetDto> GetSingleByExpressionAsync(Expression<Func<Genre, bool>>? expression = null, bool asNoTracking = false, params string[] includes);

}
