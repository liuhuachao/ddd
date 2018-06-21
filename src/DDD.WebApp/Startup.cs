using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DDD.Domain;
using DDD.Data;

namespace DDD.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // 配置 PigeonsDbContext
            var pigeonsConnectionString = Configuration["ConnectionStrings:PigeonsDbConnectionString"];
            services.AddDbContext<PigeonsContext>(options => options.UseSqlServer(pigeonsConnectionString, o => o.UseRowNumberForPaging()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
                cfg.CreateMap<Domain.Models.CmsContents, WebApp.Models.NewsDetailViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.CmsId))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.CmsTitle))
                .ForMember(d => d.Intro, o => o.MapFrom(s => s.CmsKeys))
                .ForMember(d => d.CoverImg, o => o.MapFrom(s => s.CmsPhotos))
                .ForMember(d => d.PostTime, o => o.MapFrom(s => s.OprateDate))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.CmsBody))
                .ForMember(d => d.Clicks, o => o.MapFrom(s => s.CmsClick))
                .ForMember(d => d.Likes, o => o.MapFrom(s => s.Likes))
                ;

                cfg.CreateMap<Domain.Models.VdVideo, WebApp.Models.VideoDetailViewModel>()
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
