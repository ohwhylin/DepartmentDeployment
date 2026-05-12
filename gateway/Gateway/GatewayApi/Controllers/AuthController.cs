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
    private readonly CoreAuthApiClient _coreAuthApiClient;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        LdapLookupService ldapLookupService,
        CoreAuthApiClient coreAuthApiClient,
        ILogger<AuthController> logger)
    {
        _ldapLookupService = ldapLookupService;
        _coreAuthApiClient = coreAuthApiClient;
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
            var ldapUser = _ldapLookupService.Authenticate(login.Trim(), password);

            if (ldapUser is null)
            {
                ViewBag.Error = "Неверный логин или пароль.";
                return View("~/Views/Auth/Login.cshtml");
            }

            var profile = await _coreAuthApiClient.GetProfileAsync(ldapUser.Uid);

            if (profile is null || !profile.Exists || !profile.IsActive)
            {
                ViewBag.Error = "У вас нет доступа к системе.";
                return View("~/Views/Auth/Login.cshtml");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, profile.Login),
                new Claim("uid", ldapUser.Uid),
                new Claim("cn", ldapUser.Cn ?? string.Empty),
                new Claim(ClaimTypes.Email, ldapUser.Mail ?? string.Empty)
            };

            foreach (var role in profile.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            foreach (var permission in profile.Permissions)
            {
                claims.Add(new Claim("perm", permission));
            }

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

    [HttpGet("forbidden")]
    public IActionResult ForbiddenPage()
    {
        return View("~/Views/Auth/Forbidden.cshtml");
    }
}