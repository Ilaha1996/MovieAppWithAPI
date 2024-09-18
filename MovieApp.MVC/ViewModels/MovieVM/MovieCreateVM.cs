namespace MovieApp.MVC.ViewModels.MovieVM;

public record MovieCreateVM(string Title, string Description, bool Isdeleted, int GenreId, IFormFile Image);

