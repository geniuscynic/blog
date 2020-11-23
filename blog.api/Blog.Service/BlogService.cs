using AutoMapper;
using Blog.Core;
using Blog.Core.IService;
using Blog.Core.Models;
using Blog.Core.VeiwModels;
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


        public async Task<int> Add(PostBlogViewModel blogViewModel)
        {
            //var tags = ;

            try
            {
                baseRepository.Db.Ado.BeginTran();

                var blog = mapper.Map<PostBlogViewModel, BlogArticle>(blogViewModel);

                blog.PublishDate = DateTime.Now;

                var id = await Add(blog);


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

                    blogViewModel.Tags.RemoveWhere(c => tag.Name == c);
                }

                var newTags = new List<Tag>();
                foreach (var tag in blogViewModel.Tags)
                {
                    newTags.Add(new Tag() { Name = tag.Trim() });
                }

                if (newTags.Count > 0)
                {
                    await tagRepository.Add(newTags);

                }

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


        public async Task<List<BlogArticle>> QueryPage(int pageIndex = 1, int pageSize = 20)
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

            var result = await baseRepository.Db.Queryable<BlogArticle>()
                .Mapper(it => it.Category, it => it.CategoryId)
                .Mapper((b, cache) =>
                {
                    var bts = cache.Get(ol =>
                    {
                        var allOrderIds = ol.Select(x => x.Id).ToList();

                        return baseRepository.Db.Queryable<Tag, BlogTag>((t, b) =>
                            new JoinQueryInfos(
                                JoinType.Inner, t.Id == b.TagId
                            )
                        ).Select((t, b) => new
                        {
                            id = b.BlogId,
                            tag = t
                        }).In(t => t.id, allOrderIds).ToList();

                       // return baseRepository.Db.Queryable<Tag, BlogTag>().In(it => it.BlogId, allOrderIds).ToList();//一次性查询出所有Order集合所需要的Items
                    });

                    b.Tags = bts.Where(it => it.id == b.Id).Select(t => t.tag).ToList();


                })
                .ToListAsync();
                //.Mapper((tag, cache) =>
                //{
                //    var items = cache.Get(ol =>
                //    {
                //        var allOrderIds = ol.Select(x => x.Id).ToList();

                //        return baseRepository.Db.Queryable<BlogTag>().In(it => it.BlogId, allOrderIds).ToList();//一次性查询出所有Order集合所需要的Items
                //         });

                //    items.Where(t=>t.TagId ==tag.)
                //});
      

         //.Select((b, c, bt, t) => {
         //    b.Category = c,
         //     b.Tags = t


         //}).ToListAsync();

            return result;

        }
    }
}
