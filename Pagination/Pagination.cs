
namespace Pagination
{
    public class Pagination<T>
    {
        const int DEFAULT_PAGESIZE = 10;
        const int DEFAULT_PAGE = 1;
        private int pageSize;
        private List<T> AllData { get; set; }

        public int PageSize
        {
            get
            {
                return pageSize > DEFAULT_PAGESIZE ? DEFAULT_PAGESIZE : pageSize;
            }
            set
            {
                pageSize = value;
            }
        }
        public int PageNumber { get; set; }
        public int TotalPageCount { get; set; }
        public IEnumerable<T> CurrentPage { get; set; }

        public Pagination(List<T> values, int? userPageSize = null, int? userpageNumber = null)
        {
            PageNumber = userpageNumber ?? DEFAULT_PAGE;
            pageSize = userPageSize ?? DEFAULT_PAGESIZE;
            AllData = values;
            TotalPageCount = (int)Math.Ceiling((double)AllData.Count / pageSize);

            CurrentPage = PopulateCurrentPage();
        }

        private IEnumerable<T> PopulateCurrentPage()
        {
            var paginatedValues = QueryItems(PageNumber - 1);
            CurrentPage = paginatedValues;

            return CurrentPage;
        }

        public Pagination<T> NextPage()
        {
            if (PageNumber + 1 > TotalPageCount)
            {
                CurrentPage = new List<T>();
                return this;
            }
            var paginatedValues = QueryItems(PageNumber);
            CurrentPage = paginatedValues;
            PageNumber++;

            return this;
        }

        public Pagination<T> PreviousPage()
        {
            if (PageNumber - 1 < 1)
            {
                CurrentPage = new List<T>();
                return this;
            }

            var paginatedValues = QueryItems(PageNumber - 2);
            CurrentPage = paginatedValues;
            PageNumber--;

            return this;
        }

        private IEnumerable<T> QueryItems(int numberOfPagesToSkip)
        {
            return
                AllData.Skip((numberOfPagesToSkip) * PageSize).Take(PageSize);
        }
    }
}