using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UnitOfWork.Property;
using UnitOfWork.Repository.Abstractions;
using UnitOfWork.Extensions;

namespace UnitOfWork.Repository;

/// <summary>
/// Unit of work generic class
/// </summary>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    #region Generic
    /// <summary>
    /// DB Context Property.
    /// </summary>
    private DbContext Context { get; set; }

    /// <summary>
    /// DB Set Property which initialize with T.
    /// </summary>
    private DbSet<T> DbSet { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context">DbContext</param>
    public GenericRepository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    /// <summary>
    /// Commit the changes.
    /// </summary>
    private int Commit()
    {
        return Context.SaveChanges();
    }
    #endregion

    #region Add Record
    /// <summary>
    /// Add new record of type T.
    /// </summary>
    /// <param name="entity">T</param>
    /// <returns>Task<T></returns>
    public async Task<T> Add(T entity)
    {
        entity = (await DbSet.AddAsync(entity)).Entity;
        _ = Commit();
        return entity;
    }
    #endregion

    #region Get Record(s)
    /// <summary>
    /// Get all aysc response of IEnumerable of type T.
    /// </summary>
    /// <returns>Task<IEnumerable<T>></returns>
    public async Task<IEnumerable<T>> All() => await DbSet.ToListAsync();

    public async Task<TR> FirstOrDefault<TR>(Expression<Func<T, TR>> selector,
        Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
        bool disableTracking = true)
        => await DbSet.PrepareQuery(selector: selector,
            expression: expression,
            orderBy: orderBy,
            includes: includes,
            disableTracking: disableTracking).FirstOrDefaultAsync();

    public async Task<List<TR>> Where<TR>(Expression<Func<T, TR>> selector,
        Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
        bool disableTracking = true)
        => await DbSet.PrepareQuery(selector: selector,
            expression: expression,
            orderBy: orderBy,
            includes: includes,
            disableTracking: disableTracking).ToListAsync();

    public async Task<Paged<TR>> Paged<TR>(PagedParameters pagedParams,
        Expression<Func<T, TR>> selector,
        Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
        bool disableTracking = true)
        => await DbSet.PrepareQuery(selector: selector,
               expression: expression,
               orderBy: orderBy,
               includes: includes,
               disableTracking: disableTracking).ToPagedAsync(pagedParams);
    #endregion

    #region Update Record
    /// <summary>
    /// Update exiting record of type T.
    /// </summary>
    /// <param name="entity">T</param>
    /// <returns>T</returns>
    public T Update(T entity)
    {
        entity = DbSet.Update(entity).Entity;
        _ = Commit();
        return entity;
    }
    #endregion

    #region Remove Record
    /// <summary>
    /// Delete existing record of type T based on expression
    /// </summary>
    /// <param name="expression">Expression<Func<T, bool>></param>
    /// <returns>T</returns>
    public T Remove(Expression<Func<T, bool>> expression)
    {
        T entity = DbSet.Where(expression).FirstOrDefault();
        if (null == entity)
            throw new ArgumentNullException(typeof(T).ToString(), "Record not found");
        return Delete(DbSet.Remove(entity).Entity);
    }

    /// <summary>
    /// Delete the existing record of type T.
    /// </summary>
    /// <param name="entity">T</param>
    /// <returns>T</returns>
    public T Delete(T entity)
    {
        entity = DbSet.Remove(entity).Entity;
        _ = Commit();
        return entity;
    }
    #endregion

    #region Count
    /// <summary>
    /// Get the count based on expression
    /// </summary>
    /// <param name="expression">Expression<Func<T, bool>></param>
    /// <returns>int</returns>
    public async Task<int> Count(Expression<Func<T, bool>> expression) => await DbSet.CountAsync(expression);
    #endregion


    #region Check
    /// <summary>
    /// Check the record exists or not
    /// </summary>
    /// <param name="expression">Expression<Func<T, bool>></param>
    /// <returns>Task<bool></returns>
    public async Task<bool> IsExists(Expression<Func<T, bool>> expression)
         => await DbSet.AnyAsync(expression);
    #endregion

    //#region Dispose
    //private bool disposed = false;

    //protected virtual void Dispose(bool disposing)
    //{
    //    if (!this.disposed && disposing)
    //        Context.Dispose();
    //    this.disposed = true;
    //}

    //public void Dispose()
    //{
    //    Dispose(true);
    //    GC.SuppressFinalize(this);
    //}
    //#endregion
}
