using Elsa.Activities.Email.Extensions;
using Elsa.Activities.Http.Extensions;
using Elsa.Activities.Timers.Extensions;
using Elsa.Dashboard.Extensions;
using Elsa.Extensions;
using Elsa.Persistence;
using Elsa.Persistence.EntityFrameworkCore;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Elsa.Persistence.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Elsa.Guides.Dashboard.WebApp
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
            //services.AddControllersWithViews();
            services.AddMvc();
            services.AddRouting();

            services
                // Add services used for the workflows runtime.
                .AddElsa(elsa => elsa.AddEntityFrameworkStores<SqliteContext>(ef => ef.UseSqlite(Configuration.GetConnectionString("Sqlite"), x => x.MigrationsHistoryTable("__EFMigrationsHistory","admin"))))
                .AddHttpActivities(options => options.Bind(Configuration.GetSection("Elsa:Http")))
                .AddEmailActivities(options => options.Bind(Configuration.GetSection("Elsa:Smtp")))
                .AddTimerActivities(options => options.Bind(Configuration.GetSection("Elsa:Timers")))

                // Add services used for the workflows dashboard.
                .AddElsaDashboard();
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                .UseHttpActivities()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseMvc(routes =>
                {
                  routes
                   .MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}")
                   .MapRoute(name: "area", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                 })
                .UseWelcomePage();
        }
    }
}