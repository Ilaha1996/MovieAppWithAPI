namespace MovieApp.Core.Entities;

public class Comment: BaseEntity
{
    public string Content { get; set; }
    public int MovieId { get; set; }
    public string AppUserId { get; set; }

    public Movie Movie { get; set; }
    public AppUser AppUser { get; set; }
}
