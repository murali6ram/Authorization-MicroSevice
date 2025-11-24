namespace UnitOfWork.Property;

public class PagedParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Search { get; set; } = "";
    public string OrderBy { get; set; } = "";
    public bool OrderByACS { get; set; } = false;
}
