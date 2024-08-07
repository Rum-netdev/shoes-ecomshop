namespace ShoesEShop.Handler.Shared.Pagination
{
    public class PaginationResult<T> : BaseResult
    {
        public List<T> Data { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalRecords { get; set; }

        public PaginationResult(ICollection<T> data)
        {
        }

        public PaginationResult(ICollection<T> data, int pageSize, int pageCount, int totalRecords)
        {
        }
    }
}
