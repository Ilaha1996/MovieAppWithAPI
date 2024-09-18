using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Core.Repos;
using MovieApp.Data.DAL;
using MovieApp.Data.Repos;

namespace MovieApp.Data;

public static class ServiceRegistration
{

    public static void AddRepos(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IMovieRepo, MovieRepo>();
        services.AddScoped<IGenreRepo, GenreRepo>();
        services.AddScoped<ICommentRepo, CommentRepo>();


        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
    }
}
