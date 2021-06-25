using System;
using MiBankWebsiteA2.Attributes;
using MiBankWebsiteA2.Data;
using MiBankWebsiteA2.Models;
using MiBankWebsiteA3.Controllers.BusinessObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace MiBankWebsiteA2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MiBankContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(nameof(MiBankContext)));

                // Enable lazy loading.
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<BackgroundServices>();
            services.AddScoped<RequiresLogin>();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Make the session cookie essential.
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();
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

            app.UseStatusCodePagesWithReExecute("/Error/Error", "?StatusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            
        }
    }
}
