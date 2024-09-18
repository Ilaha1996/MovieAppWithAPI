using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.DTOs.CommentDTOs;
using MovieApp.Business.Exceptions.CommonExceptions;
using MovieApp.Business.Services.Interfaces;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateDto dto)
        {

            try
            {
                await _commentService.CreateAsync(dto);
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _commentService.GetByExpressionAsync(null, true, "AppUser", "Movie"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CommentGetDto commentGetDto = null;
            try
            {
                commentGetDto = await _commentService.GetSingleByExpressionAsync(x => x.Id == id, true, "AppUser", "Movie");
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

            return Ok(commentGetDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            try
            {
                await _commentService.DeleteAsync(id);
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
