using Microsoft.AspNetCore.Mvc;
using BookCatalogAPI.Repositories.Contracts;
using BookCatalogAPI.Entities;
using BookCatalogAPI.Classes;
using BookCatalogModels.Dtos;
using BookCatalogAPI.Extensions;

namespace BookCatalogAPI.Controllers;

[ApiController]
[Route("/books")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepo;

    public BooksController(IBookRepository bookRepo)
    {
        _bookRepo = bookRepo;
    }

    /// <summary>
    /// Returns a paged list of books according to the query parameters
    /// </summary>
    /// <response code="200">Successfully returns a paged list of books</response>
    /// <response code="400">Validation error due to invalid data in the query parameters</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<BookDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResponse<BookDto>>> GetBooksAsync([FromQuery] BookGetQueryParams queryParams)
    {
        var pagedBooks = await _bookRepo.GetBooksAsync(queryParams);
        var pagedBookDtos = pagedBooks.ToDto();
        return Ok(pagedBookDtos);
    }

    /// <summary>
    /// Returns a specific book and its details
    /// </summary>
    /// <response code="200">Successfully returns the book details</response>
    /// <response code="404">The id provided doesn't match any existing book</response>
    [ActionName(nameof(GetBookAsync))]
    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDetailsDto>> GetBookAsync(int id)
    {
        var book = await _bookRepo.TryGetDetailedBookAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        var bookDto = book.ToDetailedDto();
        return Ok(bookDto);
    }

    /// <summary>
    /// Creates a new book
    /// </summary>
    /// <response code="201">Created the book successfully</response>
    /// <response code="400">Validation error due to invalid data in the body</response>
    [HttpPost]
    [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookDetailsDto>> CreateBookAsync(BookCreateDto bookCreateDto)
    {
        var newBook = await _bookRepo.CreateBookAsync(bookCreateDto);
        var detailedBook = await _bookRepo.GetDetailedBookAsync(newBook.Id);
        return CreatedAtAction(nameof(GetBookAsync), new { id = newBook.Id }, detailedBook.ToDetailedDto());
    }

    /// <summary>
    /// Creates a new book
    /// </summary>
    /// <response code="201">Created the book successfully</response>
    /// <response code="400">Validation error due to invalid data in the body</response>
    /// <response code="404">The id provided doesn't match any existing record</response>
    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDetailsDto>> UpdateBookAsync(int id, BookCreateDto dto)
    {
        var book = await _bookRepo.UpdateBookAsync(id, dto);
        if (book == null)
        {
            return NotFound();
        }

        var detailedBook = await _bookRepo.GetDetailedBookAsync(book.Id);
        return Ok(detailedBook.ToDetailedDto());
    }

    /// <summary>
    /// Deletes an existing book
    /// </summary>
    /// <response code="204">The book was successfully deleted</response>
    /// <response code="404">The id provided doesn't match any record</response>
    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteBookAsync(int id)
    {
        //Verify cascade delete behaviour
        var result = await _bookRepo.DeleteBookAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}