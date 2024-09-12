using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Business.Exceptions.CommonExceptions;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repos;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepo _genreRepo;
    private readonly IMapper _mapper;

    public GenreService(IGenreRepo genreRepo, IMapper mapper)
    {
        _genreRepo = genreRepo;
        _mapper = mapper;
    }
    public async Task CreateAsync(GenreCreateDto dto)
    {
        Genre entity = _mapper.Map<Genre>(dto);

        entity.CreatedDate = DateTime.Now;
        entity.UpdatedDate = DateTime.Now;
        await _genreRepo.CreateAsync(entity);
        await _genreRepo.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 1) throw new NotValidIdException();

        var data = await _genreRepo.GetByIdAsync(id);

        if(data == null) throw new EntityNotFoundException();

        _genreRepo.DeleteAsync(data);
        await _genreRepo.CommitAsync();
    }

    public async Task<ICollection<GenreGetDto>> GetByExpressionAsync(Expression<Func<Genre, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var datas = await _genreRepo.GetByExpressionAsync(expression, asNoTracking, includes).ToListAsync();
       
        return _mapper.Map<ICollection<GenreGetDto>>(datas);
    }

    public async Task<GenreGetDto> GetByIdAsync(int id)
    {
        if (id < 1) throw new NotValidIdException();

        var data = await _genreRepo.GetByIdAsync(id);

        if (data == null) throw new EntityNotFoundException();

        return _mapper.Map<GenreGetDto>(data);

    }
    public async Task<GenreGetDto> GetSingleByExpressionAsync(Expression<Func<Genre, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var data = await _genreRepo.GetByExpressionAsync(expression, asNoTracking, includes).FirstOrDefaultAsync();

        return _mapper.Map<GenreGetDto>(data);
    }

    public async Task<bool> IsExistAsync(Expression<Func<Genre, bool>>? expression = null)
    {
        return await _genreRepo.Table.AnyAsync(expression);
    }

    public async Task UpdateAsync(int? id, GenreUpdateDto dto)
    {
        if (id < 1) throw new NotValidIdException();

        var data = await _genreRepo.GetByIdAsync(id.Value);

        if (data == null) throw new EntityNotFoundException();

        _mapper.Map(dto,data);
        
        data.UpdatedDate = DateTime.Now;
        await _genreRepo.CommitAsync();

    }
}
