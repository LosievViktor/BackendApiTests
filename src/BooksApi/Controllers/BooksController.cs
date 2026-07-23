using BooksApi.Models;
using BooksApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers;

[ApiController]
[Route("api/books")]
[Produces("application/json")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _repository;

    public BooksController(IBookRepository repository)
    {
        _repository = repository;
    }

    /// <summary>Gets every book in the catalog.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Book>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    /// <summary>Gets a single book by its id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Book> GetById(int id)
    {
        var book = _repository.GetById(id);
        return book is null ? NotFound() : Ok(book);
    }

    /// <summary>Creates a new book.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Book> Create([FromBody] UpsertBookRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            PublishedYear = request.PublishedYear,
            Isbn = request.Isbn
        };

        var created = _repository.Add(book);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Replaces an existing book.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Book> Update(int id, [FromBody] UpsertBookRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var book = new Book
        {
            Id = id,
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            PublishedYear = request.PublishedYear,
            Isbn = request.Isbn
        };

        return _repository.TryUpdate(id, book) ? Ok(book) : NotFound();
    }

    /// <summary>Deletes a book.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        return _repository.TryDelete(id) ? NoContent() : NotFound();
    }
}
