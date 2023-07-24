# Pagination
 Quick generic pagination implementation
 
 Implementation can be found in [this class](./Pagination/Pagination.cs)

 ## Usage

``` csharp
 List<Book> allBooks = _someBookRepository.GetAllBooksFromDatabase();
 Pagination<Book> pager = new(allBooks, 5)   //pageSize = 5, page = null so it starts at page 1

 IEnumerable<Book> booksAtPageNumberOne = pager.CurrentPage; //5 books, meaning books with the Id of 1,2,3,4,5 is going to be in the IEnumerable
 IEnumerable<Book> booksOnPageTwo = pager.NextPage().CurrentPage; //books with the Id of 6,7,8,9,10 is going to be in the variable
 IEnumerable<Book> booksOnPageNumberOneAgain = pager.PreviousPage().CurrentPage; //this is the original first page with the Ids of 1,2,3,4,5 
```

### Looping
```csharp
//If the NextPage() or the PreviousPage() functions would go out of range, the CurrentPage property just returns an empty IEnumerable.
while (pager.CurrentPage.Any())
{
    var booksOnPage = pager.CurrentPage;
    foreach (var book in booksOnPage)
    {
        Console.WriteLine($"Bookd Id: {book.Id}");
        Console.WriteLine($"Book Title: {book.Title}");
        Console.WriteLine($"Book Description: {book.Description}");
        Console.WriteLine($"Book Author: {book.Author}");
    }
    pager = pager.NextPage();
}
```
