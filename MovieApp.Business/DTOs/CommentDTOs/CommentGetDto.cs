namespace MovieApp.Business.DTOs.CommentDTOs;

public record CommentGetDto(int Id,string Content, string AppUserUserName, int MovieId);

