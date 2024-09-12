namespace MovieApp.Core.Entities
{
    public class Movie:BaseEntity
    {
        public int GenreId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
    }
}
