using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using System;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using TianYu.Blog.WebMvc.Filter;
using TianYu.Core.Common;
using TianYu.Core.DataBase;

namespace TianYu.Blog.WebMvc
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60 * 60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //解决ViewBag的中文编码问题
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            services.AddControllersWithViews().AddControllersAsServices().AddRazorRuntimeCompilation();
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //})
            //.AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });
            services.AddSingleton(new AppsettingsHelper(Env.ContentRootPath));
            services.AddSqlSugarSetup();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;
                
            var servicesDllFile = Path.Combine(basePath, "TianYu.Blog.Service.dll");

            if (!File.Exists(servicesDllFile))
            {
                throw new Exception("Service.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。");
            }
              
            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                      .PropertiesAutowired()
                      .AsImplementedInterfaces()
                      .InstancePerDependency()
                      .EnableInterfaceInterceptors();//引用Autofac.Extras.DynamicProxy;
                                                     //.InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。

            //builder.RegisterType<SysButtonService>().As<ISysButtonService>().PropertiesAutowired();
            
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
            app.Use(async (context, next) =>
            {
                await next.Invoke();
            }); 
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{guid?}");

                endpoints.MapAreaControllerRoute(
                    name: "area",
                    areaName: "Admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{guid?}");
            });
        }
    }
}
