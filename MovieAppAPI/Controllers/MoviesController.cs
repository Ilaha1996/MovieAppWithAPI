using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions.CommonExceptions;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Core.Entities;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
           _movieService = movieService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _movieService.GetByExpressionAsync(null,true, "Genre"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovieCreateDto dto)
        {
            MovieGetDto movie = null;
            try
            {
                await _movieService.CreateAsync(dto);
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Created(new Uri($"api/movies/{movie.Id}",UriKind.Relative),movie);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            MovieGetDto dto = null;
            try
            {
                dto = await _movieService.GetByIdAsync(id);
            }
            catch (NotValidIdException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();               
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(dto);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id,[FromForm] MovieUpdateDto dto)
        {
            try
            {
                await _movieService.UpdateAsync(id, dto);
            }
            catch (NotValidIdException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return NoContent();

        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _movieService.DeleteAsync(id);
            }
            catch (NotValidIdException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();

        }

    }
}
