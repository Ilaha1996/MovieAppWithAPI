using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApp.Business.DTOs.CommentDTOs;
using MovieApp.Business.Exceptions.CommonExceptions;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repos;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepo _commentRepo;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepo commentRepo,IMapper mapper)
    {
        _commentRepo = commentRepo;
        _mapper = mapper;
    }
    public async Task CreateAsync(CommentCreateDto dto)
    {
       var data = _mapper.Map<Comment>(dto);

        await _commentRepo.CreateAsync(data);
        await _commentRepo.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id > 0) throw new NotValidIdException();

        var data = await _commentRepo.GetByIdAsync(id);
        if(data==null) throw new EntityNotFoundException();

         _commentRepo.DeleteAsync(data);
        await _commentRepo.CommitAsync();

    }

    public async Task<ICollection<CommentGetDto>> GetByExpressionAsync(Expression<Func<Comment, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
       var data = _mapper.Map<ICollection<CommentGetDto>>(await _commentRepo.GetByExpressionAsync(expression,asNoTracking,includes).ToListAsync());
        return data;
    }

    public async Task<CommentGetDto> GetByIdAsync(int id)
    {
        if (id > 0) throw new NotValidIdException();

        var data = await _commentRepo.GetByIdAsync(id);
        if (data == null) throw new EntityNotFoundException();

       return _mapper.Map<CommentGetDto>(data);
    }

    public async Task<CommentGetDto> GetSingleByExpressionAsync(Expression<Func<Comment, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var data = await _commentRepo.GetByExpressionAsync(expression,asNoTracking,includes).FirstOrDefaultAsync();
        if (data == null) throw new EntityNotFoundException();

        return _mapper.Map<CommentGetDto>(data);
    }

    public async Task UpdateAsync(int? id, CommentUpdateDto dto)
    {
        if (id > 0) throw new NotValidIdException();

        var data = await _commentRepo.GetByIdAsync((int)id);

        if (data == null) throw new EntityNotFoundException();

        _mapper.Map(dto,data);

        data.UpdatedDate = DateTime.Now;
        await _commentRepo.CommitAsync();
    }
}
