using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public static class Constants
    {

        public const string IdentityServerBaseAddress = "https://demo.unifiapi.com:50333/identity";
        public const string AuthorizeEndpoint = IdentityServerBaseAddress + "/connect/authorize";
        public const string LogoutEndpoint = IdentityServerBaseAddress + "/connect/endsession";
        public const string TokenEndpoint = IdentityServerBaseAddress + "/connect/token";
        public const string UserInfoEndpoint = IdentityServerBaseAddress + "/connect/userinfo";
        public const string IdentityTokenValidationEndpoint = IdentityServerBaseAddress + "/connect/identitytokenvalidation";
        public const string TokenRevocationEndpoint = IdentityServerBaseAddress + "/connect/revocation";

        public const string Host = "demo.unifiapi.com:50034";

        public const string BasePath = "/unifi/v1";

    }
}
