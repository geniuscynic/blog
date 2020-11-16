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
                .ForMember(desc => desc.Uid, opt => opt.MapFrom(src => src.Id))
                .ForMember(desc => desc.Name, opt => opt.MapFrom(src => src.NickName))
                .ForAllOtherMembers(opt => opt.Ignore());
            //.ForMember(desc => desc.Role, opt => opt.MapFrom(src => src.Roles));

            //CreateMap<Role, string>()
            //    .ForMember(desc => desc, opt => opt.MapFrom(src => src.Code))
            //    .ForAllOtherMembers(opt=> opt.Ignore());
            //CreateMap<BlogArticle, PostBlogViewModel>();
        }
    }
}
