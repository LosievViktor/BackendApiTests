# Books Library Backend API (REST + CRUD) + Automated Tests 

##  Backend API endpoints  (/src)

| Method | Route              | Description                        | Success | Failure                  |
|--------|--------------------|--------------------------------------|---------|---------------------------|
| GET    | `/api/books`       | List all books                       | 200     | —                         |
| GET    | `/api/books/{id}`  | Get a single book                    | 200     | 404 if not found          |
| POST   | `/api/books`       | Create a book                        | 201     | 400 on invalid payload    |
| PUT    | `/api/books/{id}`  | Replace an existing book             | 200     | 400 invalid / 404 missing |
| DELETE | `/api/books/{id}`  | Delete a book                        | 204     | 404 if not found          |

 
## Automation tests (/test)
1. End to end Book Crud Flow.
2. Create Book.
3. Delete Book.
4. Get Books.
5. Update Book.

Launch Backend before launching automation tests.

Stack: C#, NUnit, Refit.
