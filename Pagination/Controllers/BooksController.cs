using Microsoft.AspNetCore.Mvc;

namespace Pagination.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        const int DEFAULT_PAGESIZE = 10;

        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "books")]
        public IEnumerable<Book> GetAll(int? pageSize = null, int? page = null)
        {
            var books = GenerateBooks();
            Pagination<Book> pager = new Pagination<Book>(books, pageSize, page);

            return pager.CurrentPage;
        }

        private IEnumerable<Book> SimplePagination(List<Book> books, int? pageSize = null, int? cursor = null)
        {
            int MAX_PAGESIZE = pageSize ?? DEFAULT_PAGESIZE;
            MAX_PAGESIZE = MAX_PAGESIZE > DEFAULT_PAGESIZE ? DEFAULT_PAGESIZE : MAX_PAGESIZE;

            var cursorIndex = cursor ?? books[0].Id;
            var query = books.Where(x => x.Id >= cursorIndex).Take(MAX_PAGESIZE + 1);

            cursor = query.Last().Id;
            var pageContent = query.SkipLast(1);

            if (IsLastPage(query, MAX_PAGESIZE))
            {
                cursor = null;
                pageContent = query;
            }

            return pageContent;

        }

        private bool IsLastPage(IEnumerable<Book> query, int MAX_PAGESIZE)
        {
            return query.Count() < MAX_PAGESIZE || query.Count() % MAX_PAGESIZE == 0;
        }

        private static List<Book> GenerateBooks()
        {
            var books = new List<Book>();
            for (int i = 1; i < 15; i++)
            {
                books.Add(new Book(i, $"title_{i}", $"desc_{i}", $"author_{i}"));
            }

            return books;
        }
    }
}