using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using App.ExtendMethods;
using Microsoft.AspNetCore.Routing.Constraints;

namespace App
{
    public class Startup
    {
        public static string ContentRootPath {get;set;}
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddTransient(typeof(ILogger<>), typeof(ILogger<>));

            services.Configure<RazorViewEngineOptions>(options => {
                // /View/Controller/Action.cshtml

                // /MyView/Controller/Action.cshtml
                // {0} -> ten Action
                // {1} -> ten Controller
                // {2} -> ten Area

                
                options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);

            });

            //services.AddSingleton<ProductService>();
            services.AddSingleton(typeof(ProductService), typeof(ProductService));
            services.AddSingleton<PlanetService>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            

            app.AddStatusCodePage();// tùy biến response có lỗi 400 => 499
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // /sayhi
                endpoints.MapGet("/sayhi", async(context) =>{
                    await context.Response.WriteAsync($"Hello ASP.NET MVC {DateTime.Now}");
                });

                // endpoints.MapControllers
                // endpoints.MapcontrollerRoute
                // endpoints.MapDefaultControllerRoute
                // endpoints.MapAreaControllerRoute

                //[Route]
                //[HttpGet]
                //[HttpPost]
                //[HttpPut]
                //[HttpDelete]
                //[HttpHead]
                //[HttpPatch]

                //Area

                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "first",
                    // pattern: "xemsanpham/{id?}", // xemsanpham/1
                    pattern: "{url:regex(^((xemsanpham)|(viewproduct))$)}/{id:range(2,4)}", // xemsanpham/1
                    defaults: new { 
                        controller = "First",
                        action = "ViewProduct"
                    }
                    // constraints: new {
                    //     // url = new StringRouteConstraint("xemsanpham")
                    //     //url = new RegexRouteConstraint($"^((xemsanpham)|(viewproduct))$")
                    //     // id = new RangeRouteConstraint(2,4)
                    // }
                );


                // URL => start-here
                // controller =>
                //  action => 
                // area => 
                endpoints.MapAreaControllerRoute(
                    name: "product",
                    pattern: "/{controller}/{action=Index}/{id?}", 
                    areaName: "ProductManage"
                );
                
                //controller khong co area
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/{controller=Home}/{action=Index}/{id?}" // start-here 
                    // defaults: new { 
                    //     // controller = "First",
                    //     // action  = "ViewProduct",
                    //     // id = 3
                    // }
                );
                endpoints.MapRazorPages();
            });
        }
    }
}
