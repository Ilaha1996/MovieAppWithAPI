using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.APIResponses;
using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Business.Exceptions.CommonExceptions;
using MovieApp.Business.Exceptions.GenreExceptions;
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
            var data = await _genreService.GetByExpressionAsync(null, true);
            return Ok(new ApiResponse<ICollection<GenreGetDto>>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = data,
                ErrorMessage = null
            });
        }

        //[Authorize(Roles = "SuperAdmin,Admin,Editor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GenreCreateDto dto)
        {
            
            try
            {
                await _genreService.CreateAsync(dto);
            }
            catch(GenreAlreadyExistException ex)
            {
                return BadRequest(new ApiResponse<GenreCreateDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse<GenreCreateDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<GenreCreateDto>
            {
                Data = null,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            GenreGetDto? dto = null;
            try
            {
                dto = await _genreService.GetByIdAsync(id);
            }
            catch (NotValidIdException ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto> 
                { 
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null                
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<GenreGetDto>
                {
                    StatusCode= StatusCodes.Status404NotFound,
                    ErrorMessage = "Entity not found",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<GenreGetDto>
            {
                Data = dto,
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null
            });
        }

        //[Authorize(Roles = "SuperAdmin,Admin,Editor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] GenreUpdateDto dto)
        {
            try
            {
                await _genreService.UpdateAsync(id, dto);
            }
            catch (NotValidIdException ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Entity not found",
                    Data = null
                });
            }
            catch (GenreAlreadyExistException ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode= ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null,
                    PropertyName = ex.PropertyName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreCreateDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return NoContent();
        }
        
       // [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _genreService.DeleteAsync(id);
            }
            catch (NotValidIdException ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = ex.StatusCode,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = "Entity not found",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<GenreGetDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<GenreGetDto>
            {
                StatusCode = StatusCodes.Status200OK,
                ErrorMessage = null,
                Data = null 
            });

        }
    }
}
