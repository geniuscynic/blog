using Admin.Repository;
using Admin.Repository.User;
using Admin.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XjjXmm.FrameWork;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.ToolKit;

namespace Admin.Service
{
    public class BaseService
    {

        private IHttpContextAccessor _accessor;
        private IHttpContextAccessor Accessor
        {
            get
            {
                if (_accessor == null)
                {
                    _accessor = App.ServiceProvider.GetService<IHttpContextAccessor>();
                }

                return _accessor;
            }
        }

        private ICache _cache;
        protected ICache Cache
        {
            get
            {
                if (_cache == null)
                {
                    _cache = App.ServiceProvider.GetService<ICache>();
                }

                return _cache;
            }
        }


        public long? UserId => Accessor?.HttpContext?.User?.Identity?.Name?.ToLong();

        public UserEntity CurrentUser
        {
            get
            {
                var user = Cache.Get<UserEntity>($"{CacheKey.UserInfo}{UserId}");
                if (user == null)
                {
                    var userRepository = App.ServiceProvider.GetService<IUserRepository>();
                    user = userRepository.GetById(UserId).Result;

                    CurrentUser = user;
                }

                return user;
            }
            set
            {

                Cache.Set($"{CacheKey.UserInfo}{value.Id}", value);
            }
        }

        protected void Fill(EntityFull entity, FillStatus status)
        {
            if(entity == null)  return;
            if (status == FillStatus.Add)
            {
                entity.CreatedTime = DateTime.Now;
                entity.CreatedUserId = CurrentUser.Id;
                entity.CreatedUserName = CurrentUser.UserName;
                entity.ModifiedTime = DateTime.Now;
                entity.ModifiedUserId = CurrentUser.Id;
                entity.ModifiedUserName = CurrentUser.UserName;
            }
            else if (status == FillStatus.Update)
            {
                entity.ModifiedTime = DateTime.Now;
                entity.ModifiedUserId = CurrentUser.Id;
                entity.ModifiedUserName = CurrentUser.UserName;
            }
        }


        protected void Fill(EntityAdd entity)
        {
            entity.CreatedTime = DateTime.Now;
            entity.CreatedUserId = CurrentUser.Id;
            entity.CreatedUserName = CurrentUser.UserName;
        }


        public enum FillStatus
        {
            Add,
            Update,
        }
    }
}
