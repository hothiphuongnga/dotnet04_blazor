using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace blazor2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // 1. Đọc key, issuer và audience từ appsettings.json
            var key = Configuration["Jwt:Key"];           // Khóa bí mật để ký token
            var issuer = Configuration["Jwt:Issuer"];     // Issuer (bên phát hành token)
            var audience = Configuration["Jwt:Audience"]; // Audience (người nhận token)
            // 2. Cấu hình Authentication sử dụng JWT Bearer
            services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true, // Xác thực key bí mật của token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = true,// Xác thực Issuer 
                    ValidIssuer = issuer, // Phải khớp với Issuer trong token
                    ValidateAudience = true,    // Xác thực Audience
                    ValidAudience = audience, // Phải khớp với Audience trong token
                    ValidateLifetime = true, // Xác thực thời gian hết hạn của token
                    ClockSkew = TimeSpan.Zero, // Bỏ qua độ trễ thời gian giữa server và client (ngăn lỗi thời gian)
                    RoleClaimType = ClaimTypes.Role, // Ánh xạ claim role,
                    NameClaimType =ClaimTypes.Name // Ánh xạ claim name,
                };
            });
            // 3. Cấu hình Authorization (Phân quyền theo Role)
            services.AddAuthorization(options =>
            {
                // Chính sách chỉ cho phép Admin truy cập
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                // Chính sách chỉ cho phép User truy cập
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });






            // cấu hình cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500") // Thay đổi thành các nguồn bạn muốn cho phép
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials(); // Cho phép gửi cookie và thông tin xác thực
                });
            });


            // thên singalR
            services.AddSignalR();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            // services.AddSingleton<WeatherForecastService>();


            // DI httpclient
            services.AddHttpClient();

            services.AddHttpClient("ShoeShopApi", client =>
            {
                client.BaseAddress = new Uri("https://apistore.cybersoft.edu.vn/");
            });

            services.AddHttpClient("MovieApi", client =>
            {
                client.BaseAddress = new Uri("https://movienew.cybersoft.edu.vn/");
            });

            // Đăng ký dịch vụ CounterService với vòng đời 
            // Singleton: Dịch vụ sẽ được tạo một lần và sử dụng chung trong toàn bộ ứng dụng.
            services.AddSingleton<CounterService>();
            // đăng ký dịch vụ BurgerService
            services.AddSingleton<BurgerService>();
            // đăng ký dịch vụ CryptoService
            services.AddSingleton<CryptoService>();

            // đăng ký dịch vụ ShoeShopStateService
            services.AddSingleton<ShoeShopStateService>();

            // đăng ký dịch vụ RoomChatService
            services.AddSingleton<RoomChatService>();

            // đăng ký dịch vụ JwtService
            services.AddSingleton<JwtService>(); // tạo ra token xác thực người dùng

            // Đăng ký  Blazor.LocalStorage
            services.AddBlazoredLocalStorage();

            // Đăng ký JwtStateService để quản lý trạng thái xác thực
            services.AddScoped<JwtStateService>();

            // Cấu hình để sử dụng JwtStateService cho AuthenticationStateProvider
            services.AddScoped<AuthenticationStateProvider, JwtStateService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Bật xác thực , phan quyền

            // tuwj ddoongj chuyển về https mmặc dù gọi http 
            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            // sử dụng cors
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();

                // map Hub nào vào đây
                // vì đang để hub chung source nên "/roomHub" => url hiện tại/roomHub
                //Nếu hub ở 1 server khác thì  truywwfn 1 url khác vào ""
                endpoints.MapHub<RoomHub>("/roomHub");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
