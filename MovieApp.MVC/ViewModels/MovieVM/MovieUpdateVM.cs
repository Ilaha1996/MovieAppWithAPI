namespace MovieApp.MVC.ViewModels.MovieVM;

public record MovieUpdateVM(string Title, string Description, bool IsDeleted, int GenreId, IFormFile? Image);
