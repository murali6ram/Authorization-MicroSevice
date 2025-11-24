using IdentityServer.Business.BusinessLogic.Abstractions;

namespace IdentityServer.BusinessAccess.Abstarctions;

public interface IBusinessAccess
{
    IAccountBusinessLogic AccountBusinessLogic { get; }
}
