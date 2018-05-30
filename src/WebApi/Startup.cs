﻿using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public static IConfiguration Configuration { get; private set; }
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
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

            //api版本控制
            services.AddApiVersioning(option => {
                option.ReportApiVersions = true;
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // 配置 Repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICmsContentsRespository, CmsContentsRepository>();

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
            //services.AddDbContext<PigeonsContext>(o => o.UseSqlServer(pigeonsConnectionString);
            // 解决 EF core 生成的分页语法SQL 2008不支持的问题
            services.AddDbContext<PigeonsContext>(options => options.UseSqlServer(pigeonsConnectionString, o => o.UseRowNumberForPaging()));

            // 配置 Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info()
                {
                    Version = "v1",
                    Title = "尊贵赛鸽网 API 文档",
                    Description = "本文档严格遵循 RESTful 风格，使用 HTTP 和 HTTPS 协议",
                });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "WebApi.xml");
                c.IncludeXmlComments(xmlPath);
            });


        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            #region 配置 Automapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, Dtos.ProductCreation>();
                cfg.CreateMap<CmsContents, Dtos.NewsRead>()
                .ForMember(d => d.NewsId, o => o.MapFrom(s => s.CmsId))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.CmsTitle))
                .ForMember(d => d.Intro, o => o.MapFrom(s => s.CmsKeys))
                .ForMember(d => d.CoverImg, o => o.MapFrom(s => s.CmsPhotos))
                .ForMember(d => d.PostTime, o => o.MapFrom(s => s.OprateDate))
                ;
            });
            #endregion

            app.UseStatusCodePages();

            app.UseMvc();

            #region 配置 swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            #endregion

        }
    }
}
