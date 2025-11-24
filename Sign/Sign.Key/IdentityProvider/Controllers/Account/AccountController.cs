using IdentityProvider.Controllers.Token;
using IdentityServer.Business.Common;
using IdentityServer.Business.DataAccess.Abstarctions;
using IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Enumeration;

namespace IdentityProvider.Controllers.Account;

public class AccountController(IUnitOfWork uow, TokenController token) : Controller
{
    /// <summary>
    /// IUnitOfWork Property
    /// </summary>
    private readonly IUnitOfWork UoW = uow;

    private readonly TokenController TokenService  = token;

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        string? phoneNumber = model.PhoneNumber?.Trim().ToLower();
        string? email = model.UserEmail?.Trim().ToLower();

        var identityUser = await UoW.IdentityUser.FirstOrDefault(selector: x => x, expression: x =>
            (phoneNumber != null && x.PhoneNumber.ToLower() == phoneNumber && x.Status == Status.Active.ToString()) ||
            (email != null && x.Email.ToLower() == email && x.Status == Status.Active.ToString()));
        if (null == identityUser)
        {
            ModelState.AddModelError("", Constants.NO_SUCH_USER_EMAIL_AVAILABLE);
            return View(model);
        }

        bool otpValidation = await UoW.OtpManager.IsExists(x => x.OneTimePassword == model.Otp && x.IdentityUserId == identityUser.Id && x.Status == Status.Active.ToString());
        if (!otpValidation)
        {
            ModelState.AddModelError("", Constants.PLEASE_ENTER_VALID_OTP);
            return View(model);
        }
        var token = TokenService.GenerateToken(identityUser);
        Console.WriteLine($"{token}");

        return Redirect("http://localhost:3000/dashboard");
    }
}
