using System.Text.Json.Serialization;

namespace bShop.Shared.Models.Users;
public class LoginResponseVM
{
    [JsonPropertyName("token")] 
    public string Token { get; set; } = string.Empty;
    [JsonPropertyName("tokenExpireDate")]
    public DateTime? TokenExpireDate { get; set; }
    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = string.Empty;
    [JsonPropertyName("refreshTokenExpireDate")]
    public DateTime? RefreshTokenExpireDate { get; set; } 
}
