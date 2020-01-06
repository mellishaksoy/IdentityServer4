// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer.API.Models;

namespace IdentityServer.API
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", "Roles", new List<string>(){ "role" })
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Article.API","Article.API")

            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            var grantsForMvcClient = new List<string>();
            grantsForMvcClient.AddRange(GrantTypes.ResourceOwnerPassword);
            grantsForMvcClient.AddRange(GrantTypes.Hybrid);
            var LogoutRedirectUris = new List<string>()
            {
                "https://localhost:44341/signout-callback-oidc"
            };
            var redirectUris = new List<string>()
            {
                "https://localhost:44341/signin-oidc"
            };

            // client credentials client
            return new List<Client>
            {
                    new Client
                    {
                        ClientId = "Article.API.Client",
                        ClientName = "Article.API.Client",
                        AllowedGrantTypes =  grantsForMvcClient,
                        RedirectUris = redirectUris,
                        PostLogoutRedirectUris = LogoutRedirectUris,
                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.TokenTypes.AccessToken,
                            IdentityServerConstants.TokenTypes.IdentityToken,
                            "Article.API"
                        },
                        AllowOfflineAccess = true,
                        AllowAccessTokensViaBrowser = true,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        ClientSecrets = new List<Secret> {  new Secret("secret".Sha256()),
                        },
                    }
            };
        }

        public static List<Client> GetTestClients()
        {
            var grantsForMvcClient = new List<string>();
            grantsForMvcClient.AddRange(GrantTypes.ResourceOwnerPassword);
            grantsForMvcClient.AddRange(GrantTypes.Hybrid);

            return new List<Client>
            {
                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {

                }
            };
        }
        

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "b7b45a14-d85b-4a6d-8a07-5b345591b028",
                    Username = "admin",
                    Password = "admin123",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Admin"),
                        new Claim("website", "https://admin.com"),
                        new Claim("role", "Admin"),
                    }
                },
                new TestUser
                {
                    SubjectId = "9b2e6e20-3d6c-475c-bce1-f1c254d92c03",
                    Username = "guest",
                    Password = "qwe123",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Guest"),
                        new Claim("website", "https://admin.com"),
                        new Claim("role", "User"),
                    }
                },
                new TestUser
                {
                    SubjectId = "e7b45a14-d85b-4a6d-8a07-5b345591b02b",
                    Username = "melis",
                    Password = "Password123!",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Melis"),
                        new Claim("website", "https://admin.com"),
                        new Claim("role", "Admin"),
                    }
                }
            };
        }
    }
}