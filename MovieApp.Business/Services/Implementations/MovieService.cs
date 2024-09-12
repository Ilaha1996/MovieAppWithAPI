using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions.CommonExceptions;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repos;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Implementations;

public class MovieService : IMovieService
{
    private readonly IMovieRepo _movieRepo;
    private readonly IMapper _mapper;
    private readonly IGenreService _genreService;

    public MovieService(IMovieRepo movieRepo, IMapper mapper, IGenreService genreService)
    {
        _movieRepo = movieRepo;
        _mapper = mapper;
        _genreService = genreService;
    }
    public async Task<MovieGetDto> CreateAsync(MovieCreateDto dto)
    {
        //Movie movie = new Movie()
        //{
        //    Title = dto.Title,
        //    Description = dto.Description,
        //    CreatedDate = DateTime.Now,
        //    UpdatedDate = DateTime.Now,
        //    IsDeleted = dto.Isdeleted,
        //};asagida mapperle avtomatik cevirmisik deye bu hisseye ehtiyac yoxdur.

        if(!await _genreService.IsExistAsync(x=>x.Id == dto.GenreId && x.IsDeleted == false)) throw new EntityNotFoundException();
        
        Movie movie = _mapper.Map<Movie>(dto);
        movie.CreatedDate = DateTime.Now;
        movie.UpdatedDate = DateTime.Now;        

        await _movieRepo.CreateAsync(movie);
        await _movieRepo.CommitAsync();

        MovieGetDto getDto = _mapper.Map<MovieGetDto>(movie);

        return getDto;
    }
    public async Task DeleteAsync(int id)
    {
        if (id < 1) throw new NotValidIdException();
        var data = await _movieRepo.GetByIdAsync(id);
        if (data == null) throw new EntityNotFoundException(404, "Not Found");
        _movieRepo.DeleteAsync(data);
        await _movieRepo.CommitAsync();
    }

    public async Task<ICollection<MovieGetDto>> GetByExpressionAsync(Expression<Func<Movie, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var datas = await _movieRepo.GetByExpressionAsync(expression, asNoTracking, includes).ToListAsync();
        if (datas == null) throw new EntityNotFoundException(404, "Not Found");

        ICollection<MovieGetDto> dtos = _mapper.Map<ICollection<MovieGetDto>>(datas);
        return dtos;

    }
    public async Task<MovieGetDto> GetByIdAsync(int id)
    {
        if (id < 1) throw new NotValidIdException();
        var data = await _movieRepo.GetByIdAsync(id);
        if (data == null) throw new EntityNotFoundException(404, "Not Found");
        
        //MovieGetDto dto = new MovieGetDto(data.Id, data.Description, data.Title, data.IsDeleted, data.CreatedDate, data.UpdatedDate);

        MovieGetDto dto = _mapper.Map<MovieGetDto>(data);

        return dto;
    }

    public async Task<MovieGetDto> GetSingleByExpressionAsync(Expression<Func<Movie, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var data = await _movieRepo.GetByExpressionAsync(expression, asNoTracking, includes).FirstOrDefaultAsync();
        if (data == null) throw new EntityNotFoundException(404, "Not Found");

        MovieGetDto dto = _mapper.Map<MovieGetDto>(data);

        return dto;
    }

    public async Task UpdateAsync(int? id, MovieUpdateDto dto)
    {
        if (!await _genreService.IsExistAsync(x => x.Id == dto.GenreId && x.IsDeleted == false)) throw new EntityNotFoundException();
        if (id < 1 || id is null) throw new NotValidIdException();

        var data = await _movieRepo.GetByIdAsync((int)id);
        if (data == null) throw new EntityNotFoundException(404, "Not Found");

        _mapper.Map(dto,data);

        data.UpdatedDate = DateTime.Now;

        await _movieRepo.CommitAsync();

    }
}
