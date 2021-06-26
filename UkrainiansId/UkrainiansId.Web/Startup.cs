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
                opt.LoginPath = "/account/google-login";
            })
            .AddAmazon(options =>
            {
                options.ClientId = _configuration["Keys:Amazon:ClientId"];
                options.ClientSecret = _configuration["Keys:Amazon:ClientSecret"];
            })
            .AddApple(options =>
            {
                options.ClientId = _configuration["Keys:Apple:ClientId"];
                options.ClientSecret = _configuration["Keys:Apple:ClientSecret"];
            })
            .AddDiscord(options =>
            {
                options.ClientId = _configuration["Keys:Discord:ClientId"];
                options.ClientSecret = _configuration["Keys:Discord:ClientSecret"];
            })
            .AddFacebook(options =>
            {
                options.ClientId = _configuration["Keys:Facebook:ClientId"];
                options.ClientSecret = _configuration["Keys:Facebook:ClientSecret"];
            })
            .AddGoogle(options =>
            {
                options.ClientId = _configuration["Keys:Google:ClientId"];
                options.ClientSecret = _configuration["Keys:Google:ClientSecret"];
            })
            .AddInstagram(options =>
            {
                options.ClientId = _configuration["Keys:Instagram:ClientId"];
                options.ClientSecret = _configuration["Keys:Instagram:ClientSecret"];
            })
            .AddLinkedIn(options =>
            {
                options.ClientId = _configuration["Keys:LinkedIn:ClientId"];
                options.ClientSecret = _configuration["Keys:LinkedIn:ClientSecret"];
            })
            .AddSpotify(options =>
            {
                options.ClientId = _configuration["Keys:Spotify:ClientId"];
                options.ClientSecret = _configuration["Keys:Spotify:ClientSecret"];
            })
            .AddTwitch(options =>
            {
                options.ClientId = _configuration["Keys:Twitch:ClientId"];
                options.ClientSecret = _configuration["Keys:Twitch:ClientSecret"];
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