using System.ComponentModel.DataAnnotations;
namespace TravelApi.Models
{
  public class Review
  {

    public int ReviewId { get; set; }
    public string Description { get; set; }

    [Required]
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public int Rating { get; set; }
    public string City { get; set; }
  }
}