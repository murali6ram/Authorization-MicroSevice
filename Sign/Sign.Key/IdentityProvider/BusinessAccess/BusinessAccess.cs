using IdentityServer.Business.BusinessLogic;
using IdentityServer.Business.BusinessLogic.Abstractions;
using IdentityServer.Business.DataAccess.Abstarctions;
using IdentityServer.BusinessAccess.Abstarctions;

namespace IdentityServer.BusinessAccess;

public class BusinessAccess(IUnitOfWork uow) : IBusinessAccess 
{
    /// <summary>
    /// IUnitOfWork Property
    /// </summary>
    private readonly IUnitOfWork UoW = uow;

    public IAccountBusinessLogic AccountBusinessLogic => new AccountBusinessLogic(UoW);
}
