using IdentityModel.Models;
using UnitOfWork.Repository;

namespace IdentityServer.Business.DataAccess.Abstarctions;

public class Repository<T> : GenericRepository<T> where T : class
{
    public Repository(IdentityContext context) : base(context)
    {
    }
}
