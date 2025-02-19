// <copyright file="OidcConfigurationController.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MenuPlanner.Server.Controllers
{
    /// <summary>Auto-generated Auth class</summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OidcConfigurationController : Controller
    {
        private readonly IConfiguration _config;

        public OidcConfigurationController(IConfiguration config)
        {
            _config = config;
        }



        [HttpGet("oidc.json")]
        public IActionResult GetOidcConfiguration()
        {
            var host = Request.Host;
            var config = $"{{ \n \"authority\": \"{_config["IdentityServer:Clients:MenuPlanner.Client:Authority"]}\", \n" +
                $"\"client_id\": \"{_config["IdentityServer:Clients:MenuPlanner.Client:Id"]}\", \n" +
                $"\"redirect_uri\": \"https://{host}/authentication/login-callback\", \n" +
                $" \"post_logout_redirect_uri\": \"https://{host}/authentication/logout-callback\", \n" +
                " \"response_type\": \"code\", \n" +
                "\"scope\": \"openid profile\" ,\n " +
                $"\"audience\":\"{_config["IdentityServer:Clients:MenuPlanner.Client:Id"]}\" \n" + "}";
            return Ok(config);
        }
    }
}
