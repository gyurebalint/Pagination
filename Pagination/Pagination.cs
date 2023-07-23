
namespace Pagination
{
    public class Pagination<T>
    {
        const int DEFAULT_PAGESIZE = 10;
        const int DEFAULT_PAGE = 1;
        private int pageSize;

        public int PageNumber { get; set; }
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
        public int TotalPageCount { get; set; }
        private List<T> AllData{ get; set; }
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
            //values.Where();
            var paginatedValues = AllData
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize);
            CurrentPage = paginatedValues;

            return CurrentPage;
        }

        public Pagination<T> NextPage()
        {
            if(PageNumber + 1 > TotalPageCount)
            {
                CurrentPage = new List<T>();
                return this;
            }
            
            var paginatedValues = AllData
                .Skip((PageNumber) * PageSize)
                .Take(PageSize);
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
            
            var paginatedValues = AllData
                .Skip((PageNumber - 2) * PageSize)
                .Take(PageSize);
            CurrentPage = paginatedValues;
            PageNumber--;

            return this;
        }
    }

}

/*
     public static class PaginationExtensions
    {
        public static Pagination<Book> NextPage(this Pagination<Book> pagination)
        {
            pagination.PaginatedData = pagination.AllData.Skip(pagination.PageNumber * pagination.PageSize).Take(pagination.PageSize);
            pagination.PageNumber = pagination.PageNumber + 1;

            return pagination;
        }
    }
 */