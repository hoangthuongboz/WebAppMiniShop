using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Filters;

namespace WebApp
{
    public class Startup
    {
        IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(op =>
            {
                op.LoginPath = new PathString("/auth/signin");
                op.LogoutPath = new PathString("/auth/signout");
                op.AccessDeniedPath = new PathString("/auth/denied");
                op.ExpireTimeSpan=TimeSpan.FromDays(30);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddTransient<MenuFilter>();
            services.AddTransient<IDbConnection>(p => new SqlConnection(configuration.GetConnectionString("shop")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(p => p.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"));
            /*app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });*/
        }
    }
    
}
