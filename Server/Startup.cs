using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using CosmosOdyssey.Server.Data;
using CosmosOdyssey.Server.Controllers;

namespace CosmosOdyssey.Server
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

            services.AddControllersWithViews();
            services.AddRazorPages();

            services
                .AddSingleton<TravelPricesUpdaterService>()
                .AddHostedService(serviceCollection => serviceCollection.GetRequiredService<TravelPricesUpdaterService>());

            /*services.AddDbContext<ReservationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));*/

            services.AddDbContext<ReservationContext>(options =>
                options.UseSqlite("Data Source=reservations.db",
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                /*.EnableSensitiveDataLogging()*/);

            if (!services.Any(x => x.ServiceType == typeof(HttpClient)))
            {
                services.AddSingleton<HttpClient>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
