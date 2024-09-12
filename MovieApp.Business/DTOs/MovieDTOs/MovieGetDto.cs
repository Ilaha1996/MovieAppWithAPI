namespace MovieApp.Business.DTOs.MovieDTOs;

public record MovieGetDto(int Id,string Title, string Description,bool IsDeleted, DateTime CreatedDate, DateTime UpdatedDate, int GenreId, string GenreName);

