using MovieApp.Business.DTOs.CommentDTOs;
using MovieApp.Core.Entities;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Interfaces;

public interface ICommentService
{
    Task CreateAsync(CommentCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int? id, CommentUpdateDto dto);
    Task<CommentGetDto> GetByIdAsync(int id);
    Task<ICollection<CommentGetDto>> GetByExpressionAsync(Expression<Func<Comment, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
    Task<CommentGetDto> GetSingleByExpressionAsync(Expression<Func<Comment, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
}
