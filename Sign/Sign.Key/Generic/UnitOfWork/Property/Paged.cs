using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Property;

public class Paged<T>
{
    const int MaxPageSize = 500;

    private int _pageSize;
    public int PageSize
    {
        get => _pageSize;
        set
        {
            _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            TotalPages = WholeTotalItems;
        }
    }
    public int CurrentPage { get; set; }
    private int _totalItems;
    public int TotalItems
    {
        get => _totalItems;
        set
        {
            _totalItems = value;
            TotalPages = WholeTotalItems;
        }
    }
    public int TotalPages { get; private set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();

    private int WholeTotalItems => default == _totalItems || default == _pageSize ? 0 : (int)Math.Ceiling(_totalItems / (double)PageSize);

    public bool IsEmpty => null == this || null == Items || !Items.Any();
}