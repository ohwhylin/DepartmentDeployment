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
    private readonly ILogger<AuthController> _logger;

    public AuthController(LdapLookupService ldapLookupService, ILogger<AuthController> logger)
    {
        _ldapLookupService = ldapLookupService;
        _logger = logger;
    }

    [HttpGet("login")]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View("~/Views/Auth/Login.cshtml");
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string login, string password, string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;

        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "Введите логин и пароль.";
            return View("~/Views/Auth/Login.cshtml");
        }

        try
        {
            var user = _ldapLookupService.Authenticate(login.Trim(), password);

            if (user is null)
            {
                ViewBag.Error = "Неверный логин или пароль.";
                return View("~/Views/Auth/Login.cshtml");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Uid),
                new Claim("uid", user.Uid),
                new Claim("cn", user.Cn ?? string.Empty),
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

            return Redirect($"{Request.PathBase}/core/");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка авторизации LDAP для пользователя {Login}", login);
            ViewBag.Error = "Не удалось выполнить авторизацию. Проверьте введённые данные или настройки подключения.";
            return View("~/Views/Auth/Login.cshtml");
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect($"{Request.PathBase}/auth/login");
    }
}