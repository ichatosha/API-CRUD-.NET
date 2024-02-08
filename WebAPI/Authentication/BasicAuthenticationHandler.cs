using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace WebAPI.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>

    {

       

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
               
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.NoResult());

            // Credentials to string :
            var authHeader = Request.Headers["Authorization"].ToString();
            
            if(!authHeader.StartsWith("Basic " , StringComparison.OrdinalIgnoreCase))
                return Task.FromResult(AuthenticateResult.Fail("Invalid Scheme"));
            
            var credentialBytes = authHeader["Basic ".Length..];
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(credentialBytes));
            var UserNameAndPassword = credentials.Split(':');

            // check
            if (UserNameAndPassword[0] != "admin" || UserNameAndPassword[1] != "password")
                return Task.FromResult(AuthenticateResult.Fail("Invalid Username and Password"));

            
            // identity
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name,UserNameAndPassword[0])
            },"Basic");

            // principal >> one identity
            var pricipal = new ClaimsPrincipal(identity);

            // ticket 
            var ticket = new AuthenticationTicket(pricipal, "Basic");
            // success
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
