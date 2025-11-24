using IdentityModel.Models;
using IdentityServer.Business.DataAccess.Abstarctions;
using UnitOfWork.Repository.Abstractions;

namespace IdentityServer.Business.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IdentityContext context)
        => Context = context;

    private readonly IdentityContext Context;

    private bool disposed = false;

    private IGenericRepository<IdentityUser> _identityUser;

    private IGenericRepository<OtpManager> _otpManager;

    public IGenericRepository<IdentityUser> IdentityUser => GetRepository(_identityUser);

    public IGenericRepository<OtpManager> OtpManager => GetRepository(_otpManager);
    public IGenericRepository<T> GetRepository<T>(IGenericRepository<T> repository) where T : class
    {
        if (null == repository)
            repository = new Repository<T>(Context);
        return repository;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed && disposing)
            Context.Dispose();
        this.disposed = true;
    }
}
