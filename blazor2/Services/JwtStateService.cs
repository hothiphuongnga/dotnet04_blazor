
using System.Security.Claims;                   // Xử lý thông tin người dùng
using System.Text.Json;                         // Xử lý dữ liệu JSON
using Microsoft.AspNetCore.Components.Authorization; // Quản lý xác thực
using Blazored.LocalStorage;                    // Lưu trữ token cục bộ
using System.IdentityModel.Tokens.Jwt;          // Giải mã và xử lý JWT
using System.Threading.Tasks;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
public class JwtStateService : AuthenticationStateProvider
{
    private readonly string _key=""; // Khóa bí mật để ký token
    private readonly string _issuer=""; // Đối tượng phát hành token
    private readonly string _audience=""; // Đối tượng nhận token
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

    public JwtStateService(ILocalStorageService localStorage, IConfiguration configuration)
    {
        _localStorage = localStorage;
        _key = configuration["Jwt:Key"];
        _issuer = configuration["Jwt:Issuer"];
        _audience = configuration["Jwt:Audience"];
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Lấy token từ localStorage có key là "c_user"
        var token = await _localStorage.GetItemAsync<string>("c_user");
        // Trường hợp không có token => Không đăng nhập
        if (string.IsNullOrWhiteSpace(token))
        {
            // Trả về trạng thái không xác thực
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        try
        {
            // Cấu hình kiểm tra token
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                // Chỉ định RoleClaimType và NameClaimType khớp với cấu hình

                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name
            };
            // Giải mã token để xác thực
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            // Debug claims để kiểm tra thông tin
            foreach (var claim in principal.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            // Trả về trạng thái xác thực
            return new AuthenticationState(principal);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Token validation failed: {ex.Message}");
            // Nếu token không hợp lệ => Đăng xuất
            await _localStorage.RemoveItemAsync("c_user");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }


   public async Task MarkUserAsAuthenticated(string token)
{
    await _localStorage.SetItemAsync("c_user", token);

    // gọi lại GetAuthenticationStateAsync để lấy principal mới
    var authState = await GetAuthenticationStateAsync();
    NotifyAuthenticationStateChanged(Task.FromResult(authState));
}

public async Task MarkUserAsLoggedOut()
{
    await _localStorage.RemoveItemAsync("c_user");

    var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    NotifyAuthenticationStateChanged(Task.FromResult(authState));
}

}

