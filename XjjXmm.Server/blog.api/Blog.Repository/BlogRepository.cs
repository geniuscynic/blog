using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Entity;
using Blog.IRepository;
using SqlSugar;

namespace Blog.Repository
{
    public class BlogRepository: Repository<BlogArticle> , IBlogRepository
    {
        public BlogRepository(Dbcontext dbcontext) : base(dbcontext)
        {
        }

        public async Task<(List<BlogArticle>, int)> GetBlogList(int pageIndex = 1, int pageSize = 20)
        {
            RefAsync<int> total = 0;
            var result = await Db.Queryable<BlogArticle>()
                .Mapper(it => it.Category, it => it.CategoryId)
                .Mapper((result, cache) =>
                {

                    //var allOrderIds = ol.Select(x => x.Id).ToList();

                    var allMps = cache.Get<List<BlogTag>>(l =>
                    {
                        var blogTags = Db.Queryable<BlogTag>()
                            .Mapper(it => it.Tag, it => it.TagId)
                            .In(it => it.BlogId, l.Select(it => it.Id).ToArray())
                            .ToList();

                        return blogTags;

                    });

                    // return _defaultRepository.Db.Queryable<Tag, BlogTag>().In(it => it.BlogId, allOrderIds).ToList();//一次性查询出所有Order集合所需要的Items


                    result.Tags = allMps.Where(it => it.BlogId == result.Id).Select(it => it.Tag).ToList();


                })
                .ToPageListAsync(pageIndex, pageSize, total);

            return (result, total.Value);
        }

        public async Task<BlogArticle> GetById(int id)
        {
            var result = await Db
                .Queryable<BlogArticle>()
                .Mapper((result, cache) =>
                {
                    var allMps = cache.Get<List<BlogTag>>(l =>
                    {
                        var blogTags = Db.Queryable<BlogTag>()
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

           

            return result;
        }
    }
}
