using MovieApp.Core.Entities;
using MovieApp.Core.Repos;
using MovieApp.Data.DAL;

namespace MovieApp.Data.Repos;

public class MovieRepo : GenericRepo<Movie>, IMovieRepo
{
    public MovieRepo(AppDbContext context) : base(context) { }
    
}
