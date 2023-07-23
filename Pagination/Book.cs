namespace Pagination
{
    public class Book
    {
        public Book(int id, string title, string description, string author)
        {
            Id = id;
            Title = title;
            Description = description;
            Author = author;
        }
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Author { get; set; } = "";

    }
}