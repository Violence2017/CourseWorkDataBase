using CourseWork.Middleware;
using CourseWork.Models;
using CourseWork.Models.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CourseWork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services
                .AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connection))
                .AddDbContext<RestarauntDbContext>(options => options.UseSqlServer(connection))
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddErrorDescriber<RussianIdentityErrorDescriber>();
            services.AddControllersWithViews();
            services.AddSession().AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error").UseHsts();

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseSession()
                .UseDatabaseInitializer()
                .UseAuthentication()
                .UseAuthorization()
                .UseStatusCodePagesWithRedirects("/")
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}