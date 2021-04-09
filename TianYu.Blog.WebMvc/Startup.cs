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
            //���ViewBag�����ı�������
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
                throw new Exception("Service.dll ��ʧ����Ϊ��Ŀ�����ˣ�������Ҫ��F6���룬��F5���У����� bin �ļ��У���������");
            }
              
            // ��ȡ Service.dll ���򼯷��񣬲�ע��
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                      .PropertiesAutowired()
                      .AsImplementedInterfaces()
                      .InstancePerDependency()
                      .EnableInterfaceInterceptors();//����Autofac.Extras.DynamicProxy;
                                                     //.InterceptedBy(cacheType.ToArray());//����������������б�����ע�ᡣ

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
