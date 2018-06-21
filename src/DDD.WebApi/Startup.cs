using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using DDD.WebApi.Filters;
using DDD.WebApi.Models;
using DDD.WebApi.Repositories;
using DDD.WebApi.Services;
using DDD.Application.Interfaces;
using DDD.Application.Services;
using DDD.Application.Dtos;

namespace DDD.WebApi
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
            services.AddScoped<IHomeService,HomeService>();
            services.AddScoped<IHomesRepository, HomesRepository>();
            services.AddScoped<ICmsContentsRepository, CmsContentsRepository>();
            services.AddScoped<IVideosRepository,VideosRepository>();            

            // 配置内存缓存
            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();

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
                    Description = @"本文档遵循 RESTful 风格，使用 HTTP 和 HTTPS 协议，返回参数格式如下  
                    {
                    'code': 200,
                    'msg': '请求成功',
                    'data': { }
                    }",
                });
                c.DocumentFilter<HiddenApiFilter>();

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
            // 配置 静态文件缓存时间
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });

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

            // 配置 Automapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CmsContents, NewsList>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.CmsId))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.CmsTitle))
                .ForMember(d => d.Intro, o => o.MapFrom(s => s.CmsKeys))
                .ForMember(d => d.CoverImg, o => o.MapFrom(s => s.CmsPhotos))
                .ForMember(d => d.PostTime, o => o.MapFrom(s => s.OprateDate))
                .ForMember(d => d.ClassCode, o => o.MapFrom(s => s.CmsCode))
                ;

                cfg.CreateMap<CmsContents, NewsDetail>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.CmsId))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.CmsTitle))
                .ForMember(d => d.Intro, o => o.MapFrom(s => s.CmsKeys))
                .ForMember(d => d.CoverImg, o => o.MapFrom(s => s.CmsPhotos))
                .ForMember(d => d.PostTime, o => o.MapFrom(s => s.OprateDate))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.CmsBody))
                .ForMember(d => d.Clicks, o => o.MapFrom(s => s.CmsClick))
                .ForMember(d => d.Likes, o => o.MapFrom(s => s.Likes))
                ;

                cfg.CreateMap<VdVideo, VideoList>()
                .ForMember(d => d.Intro, o => o.MapFrom(s => s.Info))
                .ForMember(d => d.PostTime, o => o.MapFrom(s => s.UpdateTime))
                .ForMember(d => d.SourceUrl, o => o.MapFrom(s => s.VideoSource))
                ;

                cfg.CreateMap<VdVideo, VideoDetail>()
                .ForMember(d => d.Intro, o => o.MapFrom(s => s.Info))
                .ForMember(d => d.PostTime, o => o.MapFrom(s => s.UpdateTime))
                .ForMember(d => d.SourceUrl, o => o.MapFrom(s => s.VideoSource))
                .ForMember(d => d.Clicks, o => o.MapFrom(s => s.Hits))
                .ForMember(d => d.Likes, o => o.MapFrom(s => s.Likes))
                .ForMember(d => d.Duration, o => o.MapFrom(s => s.VideoLength))
                ;
            });

            app.UseStatusCodePages();

            app.UseMvc();            

            #region 配置 swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "API Document";
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            #endregion

        }
    }
}
