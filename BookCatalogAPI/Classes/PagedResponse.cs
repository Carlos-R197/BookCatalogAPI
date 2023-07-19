namespace BookCatalogAPI.Classes;

public class PagedResponse<T>
{
    public IList<T> Data { get; set; } = new List<T>();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public bool HasNextPage => Page < TotalPages;
}