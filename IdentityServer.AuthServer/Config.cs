using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>() {
                new ApiResource("resource_api1") 
                { 
                    Scopes = 
                    {
                        "api1.read" ,
                        "api1.write" ,
                        "api1.update"
                    }
                },
                new ApiResource("resource_api2")
                {
                    Scopes =
                    {
                        "api2.read" ,
                        "api2.write" ,
                        "api2.update"
                    }
                },
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
                {
                new ApiScope("api1.read", "Api1 için okuma izni"),
                new ApiScope("api1.write", "Api1 için yazma izni"),
                new ApiScope("api1.update", "Api1 için güncelleme izni"),
                new ApiScope("api2.read", "Api2 için okuma izni"),
                new ApiScope("api2.write", "Api2 için yazma izni"),
                new ApiScope("api2.update", "Api2 için güncelleme izni"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "Client1",
                    ClientSecrets = new[] { new Secret("secret".Sha256()) },
                    ClientName = "Client 1 UI App",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new[]{ "api1.read" }
                },
                new Client()
                {
                    ClientId = "Client2",
                    ClientSecrets = new[] { new Secret("secret".Sha256()) },
                    ClientName = "Client 2 UI App",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new[]{ "api1.read", "api1.update", "api2.write", "api2.update" }
                }
            };
        }
    }
}
