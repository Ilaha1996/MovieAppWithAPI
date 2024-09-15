using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Business.Exceptions.CommonExceptions;
using MovieApp.Business.Services.Interfaces;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _genreService.GetByExpressionAsync(null, true));
        }

        [Authorize(Roles = "SuperAdmin,Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GenreCreateDto dto)
        {
            
            try
            {
                await _genreService.CreateAsync(dto);
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            GenreGetDto? dto = null;
            try
            {
                dto = await _genreService.GetByIdAsync(id);
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

        [Authorize(Roles = "SuperAdmin,Admin,Editor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] GenreUpdateDto dto)
        {
            try
            {
                await _genreService.UpdateAsync(id, dto);
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
        
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _genreService.DeleteAsync(id);
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
