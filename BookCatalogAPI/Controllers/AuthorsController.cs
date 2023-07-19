using BookCatalogAPI.Extensions;
using BookCatalogAPI.Entities;
using BookCatalogAPI.Repositories.Contracts;
using BookCatalogAPI.Classes;
using BookCatalogModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogAPI.Controllers;

[ApiController]
[Route("/authors")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorRepository _authorRepo;

    public AuthorsController(IAuthorRepository authorRepo)
    {
        _authorRepo = authorRepo;
    }

    /// <summary>
    /// Returns a list of authors according to the query parameters
    /// </summary>
    /// <response code="200">Returns list of authors</response>
    /// <response code="400">Validation error(s) due to bad data in the query parameters</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<AuthorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResponse<AuthorDto>>> GetAuthorsAsync([FromQuery] AuthorGetQueryParams queryParams)
    {
        var pagedAuthors = await _authorRepo.GetAuthorsAsync(queryParams);
        var pagedAuthorDtos = pagedAuthors.ToDto();
        return Ok(pagedAuthorDtos);
    }

    /// <summary>
    /// Returns a specific author
    /// </summary>
    /// <response code="200">Returns the author</response>
    /// <response code="404">The author requested doesn't exist</response>
    [ActionName(nameof(GetAuthorAsync))]
    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorDto>> GetAuthorAsync(int id)
    {
        var author = await _authorRepo.GetAuthorAsync(id);
        if (author == null)
        {
            return NotFound();
        }
        var authorDto = author.ToDto();
        return Ok(authorDto);
    }

    /// <summary>
    /// Creates a new author
    /// </summary>
    /// <response code="201">Returns the created author</response>
    /// <response code="400">Validation error due to bad data in the body</response>
    [HttpPost]
    [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorDto>> CreateAuthorAsync(AuthorCreateDto dto)
    {
        var author = await _authorRepo.CreateAuthorAsync(dto);
        var authorDto = author.ToDto();
        return CreatedAtAction(nameof(GetAuthorAsync), new { id = authorDto.Id }, authorDto);
    }

    /// <summary>
    /// Modifies an existing author
    /// </summary>
    /// <response code="200">Retunrs the modified author</response>
    /// <response code="400">Validation error due to bad data in the body</response>
    /// <response code="404">The id provided doesn't match any existing author</response>
    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthorDto>> UpdateAuthorAsync(int id, AuthorCreateDto dto)
    {
        var author = await _authorRepo.UpdateAuthorAsync(id, dto);
        if (author == null)
            return NotFound();

        var authorDto = author.ToDto();
        return Ok(authorDto);
    }

    /// <summary>
    /// Deletes an existing author
    /// </summary>
    /// <response code="204">Author was deleted successfully</response>
    /// <response code="404">The id doesn't match any existing author</response>
    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAuthorAsync(int id)
    {
        bool result = await _authorRepo.DeleteAuthorAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}