using System.Buffers.Text;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Infrastructure.Auth;

public class BasicAuthenticationHandler 
    : AuthenticationHandler<AuthenticationSchemeOptions>
{
	public const string SchemeName = "BasicAuthentication";
    private readonly IUserRepository userRepo;

	public BasicAuthenticationHandler(
		IOptionsMonitor<AuthenticationSchemeOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder,
        ISystemClock clock,
        IUserRepository userRepository) 
		: base(options, logger, encoder, clock)
    {
        userRepo = userRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
		{
			return AuthenticateResult.Fail(
				"'Authorization' is missing from the request header");
		}

        if (!AuthenticationHeaderValue.TryParse(
				authHeaderValue.ToString(), out var authHeader))
		{
			return AuthenticateResult.Fail(
				"Unable to convert to an authentication header value");
		}

        if (!authHeader.Scheme.Equals("Basic", 
				StringComparison.OrdinalIgnoreCase))
		{
			AuthenticateResult.Fail(
				"Authentication scheme is not 'Basic'");
		}

        if (!Base64.IsValid(authHeader.Parameter!))
		{
			AuthenticateResult.Fail(
				"'Authorization' header value is not formatted correctly");
		}

		var credentialsDecoded = Encoding.UTF8.GetString(
			Convert.FromBase64String(authHeader.Parameter!));

		var credentials = credentialsDecoded.Split(':', 2);

		if (credentials.Length != 2)
		{
			return AuthenticateResult.Fail(
				"'Authorization' header value is not formatted correctly");
		}

		var username = credentials[0];
		var password = credentials[1];

        var user = await userRepo.GetUserByEmailAsync(username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
		{
			return AuthenticateResult.Fail("Invalid credentials");
		}

        var identity = new ClaimsIdentity(
			new[]
			{
				new Claim(ClaimTypes.Name, username),
				new Claim(ClaimTypes.AuthenticationMethod, authHeader.Scheme)
			},
			SchemeName);
        
        return AuthenticateResult.Success(
            new AuthenticationTicket(
                new ClaimsPrincipal(identity),
                SchemeName));
    }
}