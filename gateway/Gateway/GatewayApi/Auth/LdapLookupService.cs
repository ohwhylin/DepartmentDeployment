using System.DirectoryServices.Protocols;
using System.Net;
using Microsoft.Extensions.Options;

namespace GatewayApi.Auth;

public class LdapUserInfo
{
    public string Dn { get; set; } = string.Empty;
    public string Uid { get; set; } = string.Empty;
    public string Cn { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
}

public class LdapLookupService
{
    private readonly LdapOptions _options;

    public LdapLookupService(IOptions<LdapOptions> options)
    {
        _options = options.Value;
    }

    public LdapUserInfo? Authenticate(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            return null;

        var identifier = new LdapDirectoryIdentifier(_options.Host, _options.Port);

        using var serviceConnection = CreateConnection(identifier);
        serviceConnection.Bind(new NetworkCredential(_options.BindDn, _options.BindPassword));

        var escapedLogin = EscapeLdap(login);
        var filter = $"({_options.LoginAttribute}={escapedLogin})";

        var request = new SearchRequest(
            _options.SearchBase,
            filter,
            SearchScope.Subtree,
            new[] { "uid", "cn", "mail" });

        var response = (SearchResponse)serviceConnection.SendRequest(request);

        if (response.Entries.Count != 1)
            return null;

        var entry = response.Entries[0];
        var userDn = entry.DistinguishedName;

        using var userConnection = CreateConnection(identifier);

        try
        {
            userConnection.Bind(new NetworkCredential(userDn, password));
        }
        catch (LdapException)
        {
            return null;
        }

        return new LdapUserInfo
        {
            Dn = userDn,
            Uid = GetAttribute(entry, "uid"),
            Cn = GetAttribute(entry, "cn"),
            Mail = GetAttribute(entry, "mail")
        };
    }

    private static LdapConnection CreateConnection(LdapDirectoryIdentifier identifier)
    {
        var connection = new LdapConnection(identifier)
        {
            AuthType = AuthType.Basic
        };

        connection.SessionOptions.ProtocolVersion = 3;
        return connection;
    }

    private static string GetAttribute(SearchResultEntry entry, string name)
    {
        return entry.Attributes.Contains(name) && entry.Attributes[name]?.Count > 0
            ? entry.Attributes[name]![0]?.ToString() ?? string.Empty
            : string.Empty;
    }

    private static string EscapeLdap(string value)
    {
        return value
            .Replace("\\", "\\5c")
            .Replace("*", "\\2a")
            .Replace("(", "\\28")
            .Replace(")", "\\29")
            .Replace("\0", "\\00");
    }
}