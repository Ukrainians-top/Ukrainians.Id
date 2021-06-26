using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace UkrainiansId.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(opt =>
            {
                opt.LoginPath = "/identity/login";
            })
            .AddFacebook(options =>
            {
                options.AppId = _configuration["Keys:Facebook:ClientId"];
                options.AppSecret = _configuration["Keys:Facebook:ClientSecret"];
            })
            .AddGoogle(options =>
            {
                options.ClientId = _configuration["Keys:Google:ClientId"];
                options.ClientSecret = _configuration["Keys:Google:ClientSecret"];
            })
            .AddMicrosoftAccount(options =>
            {
                options.ClientId = _configuration["Keys:Microsoft:ClientId"];
                options.ClientSecret = _configuration["Keys:Microsoft:ClientSecret"];
            })
            .AddTwitter(options =>
            {
                options.ConsumerKey = _configuration["Keys:Twitter:ClientId"];
                options.ConsumerSecret = _configuration["Keys:Twitter:ClientSecret"];
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}