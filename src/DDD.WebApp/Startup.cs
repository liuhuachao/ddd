using DDD.Application.Interfaces;
using DDD.Application.Services;
using DDD.Data;
using DDD.Data.Repositories;
using DDD.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace DDD.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // 配置 AppService
            services.AddScoped<IHomeAppService, HomeService>();
            services.AddScoped<INewsAppService, NewsAppService>();
            services.AddScoped<IVideosAppService, VideosAppService>();

            // 配置 Repository           
            services.AddScoped<IHomesRepository, HomesRepository>();
            services.AddScoped<ICmsContentsRepository, CmsContentsRepository>();
            services.AddScoped<IVideosRepository, VideosRepository>();


            // 配置内存缓存
            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();

            // 配置 PigeonsDbContext
            var pigeonsConnectionString = Configuration["ConnectionStrings:PigeonsDbConnectionString"];
            services.AddDbContext<PigeonsContext>(options => options.UseSqlServer(pigeonsConnectionString, o => o.UseRowNumberForPaging()));
        }

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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // 配置 Automapper
            AutoMapper.Mapper.Initialize(cfg =>
            {            
                cfg.CreateMap<Domain.Entities.CmsContents, Application.Dtos.NewsDetail>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.CmsId))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.CmsTitle))
                .ForMember(d => d.Intro, o => o.MapFrom(s => s.CmsKeys))
                .ForMember(d => d.CoverImg, o => o.MapFrom(s => s.CmsPhotos))
                .ForMember(d => d.PostTime, o => o.MapFrom(s => s.OprateDate))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.CmsBody))
                .ForMember(d => d.Clicks, o => o.MapFrom(s => s.CmsClick))
                .ForMember(d => d.Likes, o => o.MapFrom(s => s.Likes))
                ;

                cfg.CreateMap<Domain.Entities.VdVideo, Application.Dtos.VideoDetail>()
                .ForMember(d => d.Intro, o => o.MapFrom(s => s.Info))
                .ForMember(d => d.PostTime, o => o.MapFrom(s => s.Uptime))
                .ForMember(d => d.SourceUrl, o => o.MapFrom(s => s.VideoSource))
                .ForMember(d => d.Clicks, o => o.MapFrom(s => s.Hits))
                .ForMember(d => d.Likes, o => o.MapFrom(s => s.Likes))
                ;
            });

        }

    }
}
