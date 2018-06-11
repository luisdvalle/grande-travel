using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TravelWebApp.Models;
using TravelWebApp.Services;

namespace TravelWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IDataService<Profile>, DataService<Profile>>();
            services.AddScoped<IDataService<TravelPackage>, DataService<TravelPackage>>();
            services.AddIdentity<IdentityUser, IdentityRole>
            (
                config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequireDigit = true;
                    config.Password.RequiredLength = 6;
                    config.Password.RequireLowercase = true;
                    config.Password.RequireUppercase = true;
                    config.Password.RequireNonAlphanumeric = true;
                }
            ).AddEntityFrameworkStores<GrandeTravelDbContext>();

            services.AddDbContext<GrandeTravelDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            //app.UseSession();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            // Call seed.
            //SeedHelpercs.Seed(app.ApplicationServices).Wait();
        }
    }
}
