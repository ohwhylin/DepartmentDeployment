using GatewayApi.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GatewayApi.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly LdapLookupService _ldapLookupService;

    public AuthController(LdapLookupService ldapLookupService)
    {
        _ldapLookupService = ldapLookupService;
    }

    [HttpGet("login")]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View("~/Views/Auth/Login.cshtml");
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string login, string? returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            ViewBag.Error = "Введите логин";
            ViewBag.ReturnUrl = returnUrl;
            return View("~/Views/Auth/Login.cshtml");
        }

        var user = _ldapLookupService.FindByLogin(login.Trim());

        if (user is null)
        {
            ViewBag.Error = "Пользователь не найден";
            ViewBag.ReturnUrl = returnUrl;
            return View("~/Views/Auth/Login.cshtml");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Uid),
            new Claim("uid", user.Uid),
            new Claim("cn", user.Cn ?? ""),
            new Claim(ClaimTypes.Email, user.Mail ?? "")
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

        return Redirect($"{Request.PathBase}/core/");
    }

    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect($"{Request.PathBase}/auth/login");
    }
}