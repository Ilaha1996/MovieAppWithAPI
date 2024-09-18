using MovieApp.MVC.ViewModels.CommentsVM;
using MovieApp.MVC.ViewModels.MovieImageVM;

namespace MovieApp.MVC.ViewModels.MovieVM;

public record MovieGetVM(int Id, string Title, string Description, bool IsDeleted, DateTime CreatedDate, DateTime UpdatedDate,
    int GenreId, string GenreName, ICollection<MovieImageGetVM> MovieImages, ICollection<CommmentGetVM> Comments);

