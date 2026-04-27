namespace GatewayApi.Auth
{
    public class LdapOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string BindDn { get; set; } = string.Empty;
        public string BindPassword { get; set; } = string.Empty;
        public string SearchBase { get; set; } = string.Empty;
        public string LoginAttribute { get; set; } = "uid";
    }
}
