using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApi.Models;
using System.Linq;


namespace TravelApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private readonly TravelApiContext _db;

    public ReviewsController(TravelApiContext db)
    {
      _db = db;
    }

    // GET api/animals
    //http://localhost:5000/api/animals?species=dinosaur&gender=female
    [HttpGet]
    //added a parameter to the method of type string that we've called species. The naming here is important as .NET
    // will automatically bind parameter values based on the query string. A call to http://localhost:5000/api/animals?species=dinosaur 
    //will now trigger our Get method and automatically bind the value "dinosaur" to the variable species. It does this by utilizing model binding
    public async Task<ActionResult<IEnumerable<Review>>> Get(string description, int rating, string city)
    {
      var query = _db.Reviews.AsQueryable();

      if (description != null)
      {
        query = query.Where(entry => entry.Description == description);
      }

      if (rating > 0)
      {
        query = query.Where(entry => entry.Rating == rating);
      }

      if (city != null)
      {
        query = query.Where(entry => entry.City == city);
      }

      return await query.ToListAsync();
    }

    // GET: api/Animals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Review>> GetReview(int id)
    {
      var review = await _db.Reviews.FindAsync(id);

      if (review == null)
      {
          return NotFound();
      }
      return review;
    }
//https://localhost:5001/api/Reviews/HighestRated
    [HttpGet("HighestRated")]
    public async Task<ActionResult<IEnumerable<Review>>> GetHighestRated()
    {
      // return _db.Reviews.Rating.ToList().Max();
      var query = _db.Reviews.AsQueryable();
      var rating = await _db.Reviews.Rating.ToListAsync();
      var topRated = rating.Max();
      query = query.Where(review => review.Rating == 5);
      return topRated;
      // return await query.ToListAsync();
    }
//Our POST route utilizes the function CreatedAtAction. This is so that it can end up returning the Animal object to the user
//, as well as update the status code to 201, for "Created", rather than the default 200 OK.
    // POST api/animals
    [HttpPost]
    public async Task<ActionResult<Review>> Post(Review review)
    {
      _db.Reviews.Add(review);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, review);
    }

    // PUT: api/Reviews/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Review review)
    {
      if (id != review.ReviewId)
      {
        return BadRequest();
      }
      _db.Entry(review).State = EntityState.Modified;
      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ReviewExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return NoContent();
    }

// forms in HTML5 don't allow for PUT, PATCH or DELETE verbs. For that reason, we had to use HttpPost along
// with an ActionName like this: [HttpPost, ActionName("Delete")]. However, we aren't using HTML anymore and
// there are no such limitations with an API. For that reason, we can use HttpPut and HttpDelete.


    // DELETE: api/Animals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
      var review = await _db.Reviews.FindAsync(id);
      if (review == null)
      {
        return NotFound();
      }
      _db.Reviews.Remove(review);
      await _db.SaveChangesAsync();
      return NoContent();
    }

// Private method for this controller
    private bool ReviewExists(int id)
    {
      return _db.Reviews.Any(e => e.ReviewId == id);
    }

  }
}

//http://localhost:5000/api/reviews