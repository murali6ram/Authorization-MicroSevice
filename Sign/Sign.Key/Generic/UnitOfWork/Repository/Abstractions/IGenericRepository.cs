using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using UnitOfWork.Property;

namespace UnitOfWork.Repository.Abstractions;

public interface IGenericRepository<T> where T : class
{
    #region Add Record
    /// <summary>
    /// Add new record of type T.
    /// </summary>
    /// <param name="entity">T</param>
    /// <returns>Task<T></returns>
    Task<T> Add(T entity);
    #endregion

    #region Get Record(s)
    /// <summary>
    /// Get all aysc response of IEnumerable of type T.
    /// </summary>
    /// <returns>Task<IEnumerable<T>></returns>
    Task<IEnumerable<T>> All();

    Task<TR> FirstOrDefault<TR>(Expression<Func<T, TR>> selector,
        Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
        bool disableTracking = true);

    Task<List<TR>> Where<TR>(Expression<Func<T, TR>> selector,
        Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
        bool disableTracking = true);

    Task<Paged<TR>> Paged<TR>(PagedParameters pagedParams,
        Expression<Func<T, TR>> selector,
        Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
        bool disableTracking = true);
    #endregion

    #region Update Record
    /// <summary>
    /// Update exiting record of type T.
    /// </summary>
    /// <param name="entity">T</param>
    /// <returns>T</returns>
    T Update(T entity);
    #endregion

    #region Remove Record
    /// <summary>
    /// Delete existing record of type T based on expression
    /// </summary>
    /// <param name="expression">Expression<Func<T, bool>></param>
    /// <returns>T</returns>
    T Remove(Expression<Func<T, bool>> expression);

    /// <summary>
    /// Delete the existing record of type T.
    /// </summary>
    /// <param name="entity">T</param>
    /// <returns>T</returns>2
    T Delete(T entity);
    #endregion

    #region Count
    /// <summary>
    /// Get the count based on expression
    /// </summary>
    /// <param name="expression">Expression<Func<T, bool>></param>
    /// <returns>Task<int></returns>
    Task<int> Count(Expression<Func<T, bool>> expression);
    #endregion

    #region Check
    /// <summary>
    /// Check the record exists or not
    /// </summary>
    /// <param name="expression">Expression<Func<T, bool>></param>
    /// <returns>Task<bool></returns>
    Task<bool> IsExists(Expression<Func<T, bool>> expression);
    #endregion
}
