namespace Common.CursorPagination;

public class PagedRequest
{
  public string? Cursor { get; set; }
  public int PageSize { get; set; }
  public bool? Backward { get; set; }
}