using IdentityModel.Models;
using UnitOfWork.Repository;
using UnitOfWork.Repository.Abstractions;

namespace IdentityServer.Business.DataAccess.Abstarctions;

public interface IUnitOfWork
{
    IGenericRepository<IdentityUser> IdentityUser { get; }

    IGenericRepository<OtpManager> OtpManager { get;  }
}
