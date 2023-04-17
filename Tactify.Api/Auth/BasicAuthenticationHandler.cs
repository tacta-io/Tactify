using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Tactify.Api.Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {          
            try 
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                if (header == null || string.IsNullOrWhiteSpace(header?.Parameter)) 
                    throw new ArgumentException("Missing Authorization header");

                var bytes = Convert.FromBase64String(header.Parameter);
                var credentials = Encoding.UTF8.GetString(bytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                if (username != "TactifyUser" || password != "TactifyPassword") 
                    throw new ArgumentException("Invalid credentials");

                var claims = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            catch (Exception exception) 
            {
                return Task.FromResult(AuthenticateResult.Fail(exception));
            }          
        }
    }
}
