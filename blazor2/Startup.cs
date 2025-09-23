using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
// tuwj ddoongj chuyển về https mmặc dù gọi http 
            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
