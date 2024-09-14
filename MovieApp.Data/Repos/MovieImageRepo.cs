using MovieApp.Core.Entities;
using MovieApp.Core.Repos;
using MovieApp.Data.DAL;

namespace MovieApp.Data.Repos;

public class MovieImageRepo : GenericRepo<MovieImage>, IMovieImageRepo
{
    public MovieImageRepo(AppDbContext context) : base(context)
    {
    }
}
