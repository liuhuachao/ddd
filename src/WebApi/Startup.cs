using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using WebApi.Entities;
using WebApi.Interfaces;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
            // 修改Mvc的配置来添加xml格式
            .AddMvcOptions(options =>
             {
                 options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
             })
            ;

            // 分别注册本地和远程日志服务
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif

            // 配置 ProductsDbContext
            var productsConnectionString = Configuration["ConnectionStrings:ProductsDbConnectionString"];
            services.AddDbContext<ProductsContext>(o => o.UseSqlServer(productsConnectionString));

            // 配置 PigeonsDbContext
            var isEncrypt = Configuration["IsEncrypt"];
            var pigeonsConnectionString = Configuration["ConnectionStrings:PigeonsDbConnectionString"];
            pigeonsConnectionString = isEncrypt == "true" ? Common.AESHelper.AESDecrypt(pigeonsConnectionString) : pigeonsConnectionString;
            services.AddDbContext<PigeonsContext>(o => o.UseSqlServer(pigeonsConnectionString));

            // 配置 Repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICmsContentsRespository, CmsContentsRepository>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // 配置 NLog
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            app.UseMvc();
        }
    }
}
