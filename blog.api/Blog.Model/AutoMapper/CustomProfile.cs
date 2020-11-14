using AutoMapper;
using Blog.Core.Models;
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

            //CreateMap<BlogArticle, PostBlogViewModel>();
        }
    }
}
