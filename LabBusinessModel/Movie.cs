
namespace LabBusinessModel.Models;

public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public MovieGenre Genre { get; set; }

    public DateTime PublishedDate { get; set; }

    public double IMDBRating { get; set; }

    public string? ImageUrl { get; set; }
}
