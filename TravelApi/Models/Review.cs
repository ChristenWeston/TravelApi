using System.ComponentModel.DataAnnotations;
namespace TravelApi.Models
{
  public class Review
  {
    // public Review()
    // {
    // }
    public int ReviewId { get; set; }
    public string Description { get; set; }
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public int Rating { get; set; }
    public int DestinationId { get; set;}
    public virtual Destination Destination { get; set; }

  }
}