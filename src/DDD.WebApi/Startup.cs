using DDD.Application.Dtos;
using DDD.Application.Interfaces;
using DDD.Application.Services;
using DDD.Data;
using DDD.Data.Repositories;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace DDD.WebApi
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

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

            // 配置 跨域Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("http://app.chsgw.com", "http://192.168.1.128:88");
                });
            });

            //api版本控制
            services.AddApiVersioning(option => {
                option.ReportApiVersions = true;
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // 配置 应用层服务
            services.AddMemoryCache();

            // 配置 内存或Redis缓存
            var isUseRedis = Configuration["CacheProvider:IsUseRedis"];      
            if (isUseRedis == "true")
            {
                services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions
                {
                    Configuration = Configuration["CacheProvider:ConnectionString"],
                    InstanceName = Configuration["CacheProvider:InstanceName"],
                }, 0));
            }
            else
            {
                services.AddSingleton<IMemoryCache>(factory =>
                {
                    var cache = new MemoryCache(new MemoryCacheOptions());
                    return cache;
                });
                services.AddSingleton<ICacheService, MemoryCacheService>();
            }

            services.AddScoped<IHomeAppService, HomeService>();
            services.AddScoped<INewsAppService, NewsAppService>();
            services.AddScoped<IVideosAppService, VideosAppService>();

            // 配置 Repository           
            services.AddScoped<IHomesRepository, HomesRepository>();
            services.AddScoped<ICmsContentsRepository, CmsContentsRepository>();
            services.AddScoped<IVideosRepository,VideosRepository>();           
                       
            // 配置 PigeonsDbContext
            var isEncrypt = Configuration["ConnectionStrings:IsEncrypt"];
            var pigeonsConnectionString = Configuration["ConnectionStrings:PigeonsDbConnectionString"];
            pigeonsConnectionString = isEncrypt == "true" ? Common.AESHelper.AESDecrypt(pigeonsConnectionString) : pigeonsConnectionString;
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
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath1 = Path.Combine(basePath, "WebApi.xml");
                var xmlPath2 = Path.Combine(basePath, "DDD.Application.xml");
                c.IncludeXmlComments(xmlPath1);
                c.IncludeXmlComments(xmlPath2);
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // 配置 静态文件
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });

            //app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStatusCodePages();            

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

            #region 配置 swagger
            app.UseSwagger();

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
