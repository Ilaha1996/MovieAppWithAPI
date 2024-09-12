using MovieApp.Core.Entities;
using MovieApp.Core.Repos;
using MovieApp.Data.DAL;

namespace MovieApp.Data.Repos;

public class GenreRepo : GenericRepo<Genre>, IGenreRepo
{
    public GenreRepo(AppDbContext context) : base(context)
    {
    }
}
