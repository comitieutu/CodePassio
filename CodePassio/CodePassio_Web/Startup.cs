using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePassio_Core;
using CodePassio_Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodePassio_Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("CodePassio_Web")));

            services.AddDefaultIdentity<ApplicationUser>().AddRoles<ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().AddRazorPagesOptions(options =>
            {
                //options.Conventions.AuthorizeAreaPage("Admin", "/Index", "AcessToAdmin");
                //options.Conventions.AuthorizeAreaFolder("Admin", "/Post", "AcessToAdmin");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AcessToAdmin",
                     policy => policy.RequireRole(Role.Manager.ToString(), Role.SuperAdmin.ToString()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
