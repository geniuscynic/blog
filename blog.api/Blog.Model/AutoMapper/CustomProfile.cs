using AutoMapper;
using Blog.Core.Models;
using Blog.Core.VeiwModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.AutoMapper
{

    /// <summary>
    /// auto map  配置类
    /// </summary>
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<PostBlogViewModel, BlogArticle>()
                .ForMember(desc => desc.Tags, opt => opt.Ignore());

            CreateMap<LoginViewModel, User>();

            CreateMap<User, TokenModelJwt>()
                .ForMember(desc => desc.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(desc => desc.Name, opt => opt.MapFrom(src => src.NickName))
                .ForAllOtherMembers(opt => opt.Ignore());


            //var configuration = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
            //   // cfg.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            //   // cfg.CreateMap<string, Type>().ConvertUsing<TypeTypeConverter>();
            //   // cfg.CreateMap<Source, Destination>();
            //});

            //configuration.AssertConfigurationIsValid();

            //var mapper = configuration.CreateMapper();

            //CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));

            CreateMap<AddMenuViewModel, Menu>()
                .ForMember(desc => desc.Name, opt => opt.MapFrom(src => src.Menu))
                .ForMember(desc => desc.ParentId, opt => opt.MapFrom(src => src.Pid));
                //.ForAllOtherMembers(opt => opt.Ignore());
            //.ForMember(desc => desc.Role, opt => opt.MapFrom(src => src.Roles));

            //CreateMap<Role, string>()
            //    .ForMember(desc => desc, opt => opt.MapFrom(src => src.Code))
            //    .ForAllOtherMembers(opt=> opt.Ignore());
            //CreateMap<BlogArticle, PostBlogViewModel>();
        }
    }
}
