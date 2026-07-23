using BooksApi.Models;

namespace BooksApi.Repositories;

public interface IBookRepository
{
    IReadOnlyCollection<Book> GetAll();

    Book? GetById(int id);

    Book Add(Book book);

    bool TryUpdate(int id, Book updatedBook);

    bool TryDelete(int id);
}
