namespace AuchanAPI.Models
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
