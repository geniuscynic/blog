﻿using Admin.Core.Common.Auth;
using Admin.Core.Common.Cache;
using Admin.Core.Common.Extensions;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using XjjXmm.FrameWork;

namespace Admin.Core.Service
{
    public abstract class BaseService: IBaseService
    {
        protected readonly object ServiceProviderLock = new object();
        protected IDictionary<Type, object> CachedServices = new Dictionary<Type, object>();
        private ICache _cache;
        private ILoggerFactory _loggerFactory;
        private IMapper _mapper;
        private IUser _user;

        /// <summary>
        /// 缓存
        /// </summary>
        public ICache Cache => LazyGetRequiredService(ref _cache);

        /// <summary>
        /// 日志工厂
        /// </summary>
        public ILoggerFactory LoggerFactory => LazyGetRequiredService(ref _loggerFactory);

        /// <summary>
        /// 映射
        /// </summary>
        public IMapper Mapper => LazyGetRequiredService(ref _mapper);

        public IServiceProvider ServiceProvider { get; set; } = App.ServiceProvider;

        /// <summary>
        /// 用户信息
        /// </summary>
        public IUser User => LazyGetRequiredService(ref _user);

        /// <summary>
        /// 日志
        /// </summary>
        protected ILogger Logger => _lazyLogger.Value;

        private Lazy<ILogger> _lazyLogger => new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

        protected TService LazyGetRequiredService<TService>(ref TService reference)
        {
            if (reference == null)
            {
                lock (ServiceProviderLock)
                {
                    if (reference == null)
                    {
                        reference = ServiceProvider.GetRequiredService<TService>();
                    }
                }
            }

            return reference;
        }

        /// <summary>
        /// 获得懒加载服务
        /// </summary>
        /// <typeparam name="TService">服务接口</typeparam>
        /// <returns></returns>
        public virtual TService LazyGetRequiredService<TService>()
        {
            return (TService)LazyGetRequiredService(typeof(TService));
        }

        /// <summary>
        /// 根据服务类型获得懒加载服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns></returns>
        public virtual object LazyGetRequiredService(Type serviceType)
        {
            return CachedServices.GetOrAdd(serviceType, () => ServiceProvider.GetRequiredService(serviceType));
        }
    }
}