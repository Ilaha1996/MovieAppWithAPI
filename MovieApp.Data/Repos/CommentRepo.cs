using MovieApp.Core.Entities;
using MovieApp.Core.Repos;
using MovieApp.Data.DAL;

namespace MovieApp.Data.Repos;

public class CommentRepo : GenericRepo<Comment>, ICommentRepo
{
    public CommentRepo(AppDbContext context) : base(context)
    {
    }
}
