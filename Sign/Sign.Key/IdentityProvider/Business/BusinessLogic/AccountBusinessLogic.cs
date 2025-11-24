using IdentityServer.Business.BusinessLogic.Abstractions;
using IdentityServer.Business.DataAccess.Abstarctions;

namespace IdentityServer.Business.BusinessLogic;

public class AccountBusinessLogic(IUnitOfWork uow) : IAccountBusinessLogic
{
    /// <summary>
    /// IUnitOfWork Property
    /// </summary>
    private readonly IUnitOfWork UoW = uow;
}
