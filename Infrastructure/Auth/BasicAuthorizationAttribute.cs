using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Auth;

public class BasicAuthorizationAttribute : AuthorizeAttribute
{
	public BasicAuthorizationAttribute()
	{
		AuthenticationSchemes = 
			BasicAuthenticationHandler.SchemeName;
	}
}
