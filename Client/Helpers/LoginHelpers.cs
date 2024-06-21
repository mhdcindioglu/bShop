using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace bShop.Client.Helpers;

public static class LoginHelpers
{
    public static string GenerateTokenString(ClaimsPrincipal user, TimeSpan expireAfter, string JwtKey, List<Claim>? newClaims = null)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
        var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        
        var claims = new List<Claim>();
        claims.AddRange(user.Claims);
        if(newClaims != null)
            claims.AddRange(newClaims);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.Add(expireAfter),
            signingCredentials: signingCred
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString;
    }

    public static string GenerateRefreshTokenString()
    {
        var randomNumber = new byte[64];
        using (var numberGenerator = RandomNumberGenerator.Create())
            numberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public static ClaimsPrincipal? GetTokenPrincipal(string token, string JwtKey)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));

        var validation = new TokenValidationParameters
        {
            IssuerSigningKey = securityKey,
            ValidateLifetime = false,
            ValidateActor = false,
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
    }

    public static ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        if (handler.ReadToken(token) is not JwtSecurityToken jsonToken)
            throw new ArgumentException("Invalid JWT token");

        var claims = new List<Claim>();
        foreach (var claim in jsonToken.Claims)
            claims.Add(claim);

        var identity = new ClaimsIdentity(claims, "jwt");
        var principal = new ClaimsPrincipal(identity);

        return principal;
    }

    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}