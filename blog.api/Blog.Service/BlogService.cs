﻿using AutoMapper;
using Blog.Core;
using Blog.Core.IService;
using Blog.Core.Models;
using Blog.Core.ViewModels;
using Blog.Repository.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class BlogService : BaseServices<BlogArticle>, IBlogService
    {
        //private readonly IBaseRepository<BlogArticle> blogRepository;
        //private readonly IMapper mapper;
        private readonly IBaseRepository<Tag> tagRepository;
        private readonly IBaseRepository<BlogTag> blogTagRepository;

        protected override IBaseRepository<BlogArticle> baseRepository { get; set; }


        public BlogService(IBaseRepository<BlogArticle> blogRepository, IMapper mapper, IBaseRepository<Tag> tagRepository, IBaseRepository<BlogTag> blogTagRepository)
            : base(blogRepository, mapper)
        {
            //baseRepository = blogRepository;
            //this.mapper = mapper;
            this.tagRepository = tagRepository;
            this.blogTagRepository = blogTagRepository;
        }


        public async Task<int> Save(PostBlogViewModel blogViewModel)
        {
            //var tags = ;

            try
            {
                baseRepository.Db.Ado.BeginTran();

                var blog = mapper.Map<PostBlogViewModel, BlogArticle>(blogViewModel);

                blog.PublishDate = DateTime.Now;

                var id = blog.Id;
                if (blog.Id == 0)
                {
                    id = await Add(blog);
                }
                else
                {
                    await Edit(blog);

                    await blogTagRepository.Db.Deleteable<BlogTag>().Where(t => t.BlogId == blog.Id).ExecuteCommandAsync();
                }


                var tags = await tagRepository.Query(t => blogViewModel.Tags.Contains(t.Name));

                var blogTags = new List<BlogTag>();
                foreach (var tag in tags)
                {
                    var temp = new BlogTag
                    {
                        BlogId = id,
                        TagId = tag.Id,

                    };

                    blogTags.Add(temp);

                    //if(blogViewModel.Tags.Contains(tag.Name))
                    //{
                    //    blogViewModel.Tags.Remove(tag.Name);
                    //}

                    var res = blogViewModel.Tags.RemoveWhere(c => tag.Name.Trim() == c.Trim());
                }

                var newTags = new List<Tag>();
                foreach (var tag in blogViewModel.Tags)
                {
                    var newTag = new Tag() { Name = tag.Trim() };
                    newTag.Id = await tagRepository.Add(newTag);
                    newTags.Add(newTag);
                }

                //if (newTags.Count > 0)
                //{
                //    await tagRepository.Add(newTags);

                //}

                foreach (var tag in newTags)
                {
                    var temp = new BlogTag
                    {
                        BlogId = id,
                        TagId = tag.Id,

                    };

                    blogTags.Add(temp);
                }

                if (blogTags.Count > 0)
                {
                    await blogTagRepository.Add(blogTags);
                }

                //throw new Exception("dd");
                baseRepository.Db.Ado.CommitTran();

                return id;
            }
            catch (Exception)
            {
                baseRepository.Db.Ado.RollbackTran();
                throw;
            }

        }


        public async Task<PageModel<ListBlogViewModel>> GetBlogList(int pageIndex = 1, int pageSize = 20)
        {
            /* var result = await baseRepository.Db.Queryable<BlogArticle, Category, BlogTag, Tag>(
                 (b, c, bt, t) =>
                   new JoinQueryInfos(
                       JoinType.Left, b.CategoryId == c.Id,
                       JoinType.Left, b.Id == bt.BlogId,
                       JoinType.Left, bt.TagId == t.Id
                       )

           ).Select((b,c,bt,t)=> { 
               b.Category = c,
               b.Tags = t

           }).ToListAsync();*/

            //var result = await baseRepository.Db.Queryable<BlogArticle>()
            //    .Mapper(it => it.Category, it => it.CategoryId)
            //    .Mapper((b, cache) =>
            //    {
            //        var bts = cache.Get(ol =>
            //        {
            //            var allOrderIds = ol.Select(x => x.Id).ToList();

            //            return baseRepository.Db.Queryable<Tag, BlogTag>((t, b) =>
            //                new JoinQueryInfos(
            //                    JoinType.Inner, t.Id == b.TagId
            //                )
            //            ).Select((t, b) => new
            //            {
            //                id = b.BlogId,
            //                tag = t
            //            }).In(t => t.id, allOrderIds).ToList();

            //           // return baseRepository.Db.Queryable<Tag, BlogTag>().In(it => it.BlogId, allOrderIds).ToList();//一次性查询出所有Order集合所需要的Items
            //        });

            //        b.Tags = bts.Where(it => it.id == b.Id).Select(t => t.tag).ToList();


            //    })
            //    .ToListAsync();

            RefAsync<int> total = 0;
            var result = await baseRepository.Db.Queryable<BlogArticle>()
               .Mapper(it => it.Category, it => it.CategoryId)
               .Mapper((result, cache) =>
               {

                   //var allOrderIds = ol.Select(x => x.Id).ToList();

                   var allMps = cache.Get<List<BlogTag>>(l =>
                   {
                       var blogTags = baseRepository.Db.Queryable<BlogTag>()
                                             .Mapper(it => it.Tag, it => it.TagId)
                                             .In(it => it.BlogId, l.Select(it => it.Id).ToArray())
                                             .ToList();

                       return blogTags;

                   });

                   // return baseRepository.Db.Queryable<Tag, BlogTag>().In(it => it.BlogId, allOrderIds).ToList();//一次性查询出所有Order集合所需要的Items


                   result.Tags = allMps.Where(it => it.BlogId == result.Id).Select(it => it.Tag).ToList();


               })
               .ToPageListAsync(pageIndex, pageSize, total);
            //.ToListAsync();

            //.Mapper((tag, cache) =>
            //{
            //    var items = cache.Get(ol =>
            //    {
            //        var allOrderIds = ol.Select(x => x.Id).ToList();

            //        return baseRepository.Db.Queryable<BlogTag>().In(it => it.BlogId, allOrderIds).ToList();//一次性查询出所有Order集合所需要的Items
            //         });

            //    items.Where(t=>t.TagId ==tag.)
            //});


            var blogs = mapper.Map<List<BlogArticle>, List<ListBlogViewModel>>(result);
            //.Select((b, c, bt, t) => {
            //    b.Category = c,
            //     b.Tags = t


            //}).ToListAsync();

            return new PageModel<ListBlogViewModel>
            {
                DataCount = total.Value,
                Page = pageIndex,
                PageSize = pageSize,
                Data = blogs,

            };

        }

        public async Task<PostBlogViewModel> Get(int id)
        {
            var result = await baseRepository.Db
                .Queryable<BlogArticle>()
                .Mapper((result, cache) =>
                {
                    var allMps = cache.Get<List<BlogTag>>(l =>
                    {
                        var blogTags = baseRepository.Db.Queryable<BlogTag>()
                                              .Mapper(it => it.Tag, it => it.TagId)
                                              .In(it => it.BlogId, l.Select(it => it.Id).ToArray())
                                              .ToList();

                        return blogTags;

                    });


                    result.Tags = allMps.Where(it => it.BlogId == result.Id).Select(it => it.Tag).ToList();
                })
                //.Where(t => t.Id == id)
                //.FirstAsync();
                //.FirstAsync(t => t.Id == id);
                .InSingleAsync(id);

            var blog = mapper.Map<BlogArticle, PostBlogViewModel>(result);



            result.Tags.ForEach(t => blog.Tags.Add(t.Name));


            return blog;

        }
    }
}
