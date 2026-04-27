using System.Security.Claims;
using GatewayApi.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace GatewayApi.Controllers;

public class AuthController : Controller
{
    private readonly LdapLookupService _ldapLookupService;

    public AuthController(LdapLookupService ldapLookupService)
    {
        _ldapLookupService = ldapLookupService;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string login, string? returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            ViewBag.Error = "Введите логин";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        var user = _ldapLookupService.FindByLogin(login.Trim());

        if (user is null)
        {
            ViewBag.Error = "Пользователь не найден в LDAP";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Uid),
            new Claim("uid", user.Uid),
            new Claim("cn", user.Cn),
            new Claim(ClaimTypes.Email, user.Mail ?? string.Empty)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        if (!string.IsNullOrWhiteSpace(returnUrl))
            return Redirect(returnUrl);

        return Redirect("/core/Home/Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/auth/login");
    }
}