using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions.CommonExceptions;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Business.Utilities;
using MovieApp.Core.Entities;
using MovieApp.Core.Repos;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Implementations;

public class MovieService : IMovieService
{
    private readonly IMovieRepo _movieRepo;
    private readonly IMapper _mapper;
    private readonly IGenreService _genreService;
    private readonly IWebHostEnvironment _env;

    public MovieService(IMovieRepo movieRepo, IMapper mapper, IGenreService genreService, IWebHostEnvironment env)
    {
        _movieRepo = movieRepo;
        _mapper = mapper;
        _genreService = genreService;
        _env = env;
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

        string ImageUrl = dto.Image.SaveFile(_env.WebRootPath, "uploads");
        movie.MovieImages = new List<MovieImage>();
        MovieImage movieImage = new MovieImage()
        {
            ImageUrl = ImageUrl,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            IsDeleted = false,
            // Movie = movie, ya bele yaza bilersen yada yuxaridaki kimi sekilleri newlayib,asagidaki kimi add ederek.
        };
        movie.MovieImages.Add(movieImage);

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

        string ImageUrl = dto.Image.SaveFile(_env.WebRootPath, "uploads");
        data.MovieImages = new List<MovieImage>();
        MovieImage movieImage = new MovieImage()
        {
            ImageUrl = ImageUrl,
            UpdatedDate = DateTime.Now,
            IsDeleted = false,
            // Movie = movie, ya bele yaza bilersen yada yuxaridaki kimi sekilleri newlayib,asagidaki kimi add ederek.
        };
        data.MovieImages.Add(movieImage);

        _mapper.Map(dto,data);

        data.UpdatedDate = DateTime.Now;

        await _movieRepo.CommitAsync();

    }
}
