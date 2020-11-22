using AutoMapper;
using Blog.Core.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog.Common.Extensions.AutoMapper
{
    /// <summary>
    /// 静态全局 AutoMapper 配置文件
    /// </summary>
    public static  class AutoMapperConfig
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {

            //var configuration = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
            //    // cfg.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            //    // cfg.CreateMap<string, Type>().ConvertUsing<TypeTypeConverter>();
            //    // cfg.CreateMap<Source, Destination>();
            //});

            //configuration.AssertConfigurationIsValid();

            
            //services.AddAutoMapper(typeof(AutoMapperConfig));
            //AutoMapperConfig.RegisterMappings();

            // new MapperConfiguration(cfg =>
            // {
            //   cfg.AddProfile(new CustomProfile());
            //});

            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new CustomProfile());
            //});

            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);

            services.AddAutoMapper(typeof(CustomProfile));
        }

        //public static MapperConfiguration RegisterMappings()
        //{
        //    return new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile(new CustomProfile());
        //    });
        //}
    }
}
