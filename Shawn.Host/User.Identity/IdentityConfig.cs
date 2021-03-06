using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace User.Identity
{
    public class IdentityConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("getway_api", "user service")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            //Dictionary<string, string> clientsUrl
            #region MyRegion

            //var mvcHybrid = clientsUrl["mvcHybrid"];
            //var mvcImp = clientsUrl["mvcImp"];

            //return new[]
            //{
            //    // client credentials flow client
            //    new Client
            //    {
            //        ClientId = "client",
            //        ClientName = "Client Credentials Client",
            //        //jwt过期时间
            //        AccessTokenLifetime = 600,
            //        AllowedGrantTypes = GrantTypes.ClientCredentials,
            //        ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

            //        AllowedScopes = { "api1" }
            //    },

            //    new Client()
            //    {
            //        ClientId = "ro.client",
            //        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

            //        ClientSecrets =
            //        {
            //            new Secret("secret".Sha256())
            //        },
            //        AllowedScopes = { "api1" }
            //    },

            //    //// MVC client using hybrid flow
            //    //new Client
            //    //{
            //    //    ClientId = "mvc",
            //    //    ClientName = "MVC Client",

            //    //    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
            //    //    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

            //    //    RedirectUris = { "http://localhost:5001/signin-oidc" },
            //    //    FrontChannelLogoutUri = "http://localhost:5001/signout-oidc",
            //    //    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

            //    //    AllowOfflineAccess = true,
            //    //    AllowedScopes = { "openid", "profile", "api1" }
            //    //},

            //    //// SPA client using implicit flow
            //    //new Client
            //    //{
            //    //    ClientId = "spa",
            //    //    ClientName = "SPA Client",
            //    //    ClientUri = "http://identityserver.io",

            //    //    AllowedGrantTypes = GrantTypes.Implicit,
            //    //    AllowAccessTokensViaBrowser = true,

            //    //    RedirectUris =
            //    //    {
            //    //        "http://localhost:5002/index.html",
            //    //        "http://localhost:5002/callback.html",
            //    //        "http://localhost:5002/silent.html",
            //    //        "http://localhost:5002/popup.html",
            //    //    },

            //    //    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
            //    //    AllowedCorsOrigins = { "http://localhost:5002" },

            //    //    AllowedScopes = { "openid", "profile", "api1" }
            //    //},
            //    new Client()
            //    {
            //        ClientId = "mvcImp",
            //        ClientName = "MVC ClientImp",
            //        AllowedGrantTypes = GrantTypes.Implicit,

            //        // 登录成功回调处理地址，处理回调返回的数据
            //        RedirectUris = { $"{mvcImp}/signin-oidc" },

            //        // where to redirect to after logout
            //        PostLogoutRedirectUris = {  $"{mvcImp}/signout-callback-oidc" },
            //        ClientSecrets = { new Secret("shawn".Sha256()) },
            //        RequireConsent = true,
            //        AllowedScopes = new List<string>
            //        {
            //            IdentityServerConstants.StandardScopes.OpenId,
            //            IdentityServerConstants.StandardScopes.Profile,
            //            "api1"
            //        },
            //        AccessTokenType = AccessTokenType.Jwt,
            //        AllowAccessTokensViaBrowser = true
            //    },
            //    new Client()
            //    {
            //        ClientId = "mvcHybrid",
            //        ClientName = "MVC Client Hybrid",
            //        ClientUri=$"{mvcHybrid}",
            //        AllowedGrantTypes = GrantTypes.Hybrid,
            //        ClientSecrets =
            //        {
            //            new Secret("secret".Sha256())
            //        },
            //        RedirectUris           = { $"{mvcHybrid}/signin-oidc" },
            //        PostLogoutRedirectUris = { $"{mvcHybrid}/signout-callback-oidc" },
            //        //如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
            //        AllowedScopes = { "openid", "profile", "api1","email", OidcConstants.StandardScopes.OfflineAccess},
            //        AccessTokenLifetime = 600, // 60s
            //        IdentityTokenLifetime= 600,
            //        AllowOfflineAccess=true,
            //        AllowAccessTokensViaBrowser = false,
            //        AbsoluteRefreshTokenLifetime = 2592000,//RefreshToken的最长生命周期,默认30天
            //        RefreshTokenExpiration = TokenExpiration.Sliding,//刷新令牌时，将刷新RefreshToken的生命周期。RefreshToken的总生命周期不会超过AbsoluteRefreshTokenLifetime。
            //    }
            //};

            #endregion

            return new List<Client>
            {
                new Client
                {
                    ClientId = "Iphone",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AccessTokenLifetime = 7200,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AllowOfflineAccess = true,
                    RequireClientSecret = false,
                    AllowedGrantTypes = new List<string>{"sms_code"},
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes = new List<string>
                    {
                        "getway_api",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    }
                }
            };
        }
    }
}
