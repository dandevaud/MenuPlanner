// <copyright file="CustomAuhtorizationMessageHandler.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace MenuPlanner.Client.Shared
{
   

    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager)
            : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[] { navigationManager.BaseUri, "https://sts-uat.ddev.ch"},
                scopes: new[] { "openid","profile", "MenuPlanner_oidc" },
                
                returnUrl: navigationManager.Uri);
        }
    }
}
