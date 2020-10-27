using AutoMapper;
using Blog.Core.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Common.Extensions.AutoMapper
{
    /// <summary>
    /// 静态全局 AutoMapper 配置文件
    /// </summary>
    public static  class AutoMapperConfig
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {


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
