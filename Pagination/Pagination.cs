
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
        public List<T> AllData{ get; set; }
        public IEnumerable<T> PaginatedData { get; set; }

        public Pagination(List<T> values, int? userPageSize = null, int? userpageNumber = null, int? userCursor = null)
        {
            PageNumber = userpageNumber ?? DEFAULT_PAGE;
            pageSize = userPageSize ?? DEFAULT_PAGESIZE;
            AllData = values;
            TotalPageCount = (int)Math.Ceiling((double)AllData.Count / pageSize);

            PaginatedData = PopulateData();
        }

        private IEnumerable<T> PopulateData()
        {
            //values.Where();
            var paginatedValues = AllData
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize);
            PaginatedData = paginatedValues;

            return PaginatedData;
        }

        public IEnumerable<T> NextPage()
        {
            if(PageNumber + 1 > TotalPageCount)
                return new List<T>();

            var paginatedValues = AllData
                .Skip((PageNumber) * PageSize)
                .Take(PageSize);
            PageNumber++;
            PaginatedData = paginatedValues;

            return PaginatedData;
        }

        public IEnumerable<T> PreviousPage()
        {
            if (PageNumber - 1 < 1)
                return new List<T>();

            var paginatedValues = AllData
                .Skip((PageNumber) * PageSize)
                .Take(PageSize);
            PageNumber--;
            PaginatedData = paginatedValues;

            return PaginatedData;
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