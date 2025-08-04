using LabBusinessModel.Models;
using LabBusinessModel.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabMovieApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _service;

    public MoviesController(IMovieService service)
    {
        _service = service;
    }

    // GET: api/<MovieController>
    [HttpGet]
    public IList<Movie> GetMovies()
            => _service.GetMovies();

    // GET api/<MovieController>/5
    [HttpGet]
    [Route("{id:int:min(1)}")]
    public Movie? GetById(int id)
            => _service.GetMovie(id);

    // POST api/<MovieController>
    [HttpPost]
    public void AddMovie(Movie movie)
        => _service.AddMovie(movie);

    // PUT api/<MovieController>/5
    [HttpPut]
    [Route("{id:int:min(1)}")]
    public void UpdateMovie(int id, Movie movie)
        => _service.UpdateMove(id, movie);

    // DELETE api/<MovieController>/5
    [HttpDelete]
    [Route("{id:int:min(1)}")]
    public void DeleteMovie(int id)
        => _service.DeleteMovie(id);
}
