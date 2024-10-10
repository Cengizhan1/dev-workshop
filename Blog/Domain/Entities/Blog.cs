namespace BlogDomain.Entities;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // user eklenecek

    // blog category eklenecek


    
}
