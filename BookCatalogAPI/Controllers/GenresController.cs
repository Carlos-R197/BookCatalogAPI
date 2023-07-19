using Microsoft.AspNetCore.Mvc;
using BookCatalogAPI.Repositories.Contracts;
using BookCatalogAPI.Extensions;
using BookCatalogModels.Dtos;

namespace BookCatalogAPI.Controllers;

[ApiController]
[Route("/genres")]
public class GenresController : ControllerBase
{
    private readonly IGenreRepository _genreRepo;

    public GenresController(IGenreRepository genreRepo)
    {
        _genreRepo = genreRepo;
    }

    /// <summary>
    /// Returns all genres according to the query parameters.
    /// </summary>
    /// <response code="200">Returns list of genres</response>
    /// <response code="400">Validation error from the query parameters</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GenreDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllGenresAsync([FromQuery] GenreGetQueryParams queryParams)
    {
        var genres = await _genreRepo.GetAllGenresAsync(queryParams);
        var genreDtos = genres.Select(genre => genre.ToDto());
        return Ok(genreDtos);
    }

    /// <summary>
    /// Returns a specific genre based on the id
    /// </summary>
    /// <response code="200">The specific genre requested</response>
    /// <response code="404">The genre requested doesn't exist</response>
    [ActionName(nameof(GetGenreAsync))]
    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenreDto>> GetGenreAsync(int id)
    {
        var genre = await _genreRepo.GetGenreAsync(id);
        if (genre == null)
        {
            return NotFound();
        }
        var genreDto = genre.ToDto();
        return Ok(genreDto);
    }

    /// <summary>
    /// Creates a new genre
    /// </summary>
    /// <response code="201">Created the new genre successfully. Returns the new genre</response>
    /// <response code="400">Validation error</response>
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<GenreDto>> CreateGenreAsync(GenreCreateDto dto)
    {
        var genre = await _genreRepo.CreateGenreAsync(dto);
        var genreDto = genre.ToDto();
        return CreatedAtAction(nameof(GetGenreAsync), new { id = genreDto.Id }, genreDto);
    }

    /// <summary>
    /// Modifies an existing genre with the data provided in the body
    /// </summary>
    /// <response code="200">Returns the updated genre</response>
    /// <response code="400">Validation error due to bad data in the body</response>
    /// <response code="404">The id provided doesn't match any genre</response>
    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenreDto>> UpdateGenreAsync(int id, GenreCreateDto dto)
    {
        var genre = await _genreRepo.GetGenreAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        var updatedGenre = await _genreRepo.UpdateGenreAsync(id, dto);
        var genreDto = updatedGenre.ToDto();
        return Ok(genreDto);
    }

    /// <summary>
    /// Deletes an existing genre
    /// </summary>
    /// <response code="204">The genre was deleted successfully</response>
    /// <response code="404">The genre you attempted to delete doesn't exist</response>
    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteGenreAsync(int id)
    {
        bool result = await _genreRepo.DeleteGenreAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}