using System.Collections.Concurrent;
using BooksApi.Models;

namespace BooksApi.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly ConcurrentDictionary<int, Book> _books = new();
    private int _nextId;

    public InMemoryBookRepository()
    {
        Seed();
    }

    public IReadOnlyCollection<Book> GetAll() =>
        _books.Values.OrderBy(b => b.Id).ToList();

    public Book? GetById(int id) =>
        _books.TryGetValue(id, out var book) ? book : null;

    public Book Add(Book book)
    {
        book.Id = Interlocked.Increment(ref _nextId);
        _books[book.Id] = book;
        return book;
    }

    public bool TryUpdate(int id, Book updatedBook)
    {
        if (!_books.ContainsKey(id))
        {
            return false;
        }

        updatedBook.Id = id;
        _books[id] = updatedBook;
        return true;
    }

    public bool TryDelete(int id) => _books.TryRemove(id, out _);

    private void Seed()
    {
        Add(new Book { Title = "Clean Code", Author = "Robert C. Martin", Genre = "Software Engineering", PublishedYear = 2008, Isbn = "978-0132350884" });
        Add(new Book { Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Genre = "Software Engineering", PublishedYear = 1999, Isbn = "978-0201616224" });
        Add(new Book { Title = "Dune", Author = "Frank Herbert", Genre = "Science Fiction", PublishedYear = 1965, Isbn = "978-0441013593" });
    }
}
