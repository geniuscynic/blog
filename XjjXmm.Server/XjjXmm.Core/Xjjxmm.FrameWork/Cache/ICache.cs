using System;
using System.Collections.Generic;

namespace XjjXmm.FrameWork.Cache
{
    public interface ICache
    {
        /// <summary>
        /// 从缓存中获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>缓存对象</returns>
        T Get<T>(string key);


        /// <summary>
        /// 数据对象装箱缓存
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">数据对象</param>
        /// <param name="expire">过期时间</param>
        bool Set(string key, object value, TimeSpan expiresIn, bool isSliding = false);

        /// <summary>
        /// 添加到缓存
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>结果状态</returns>
        bool Set(string key, object value);

        /// <summary>
        /// 添加到缓存
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns>结果状态</returns>
        //T Set<T>(string key, T value, DateTime expiresAt);

        /// <summary>
        /// 获取键的集合
        /// </summary>
        /// <returns>键的集合</returns>
        List<string> Keys();



        /// <summary>
        /// 判断缓存中是否有此对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否存在</returns>
        bool Contain(string key);



        /// <summary>
        /// 数据对象从缓存对象中移除
        /// </summary>
        /// <param name="key">键</param>
        void Remove(string key);

        void RemoveAll(IEnumerable<string> keys);

        /// <summary>
        /// 清除实例
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取缓存对象集合
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <returns>缓存对象集合</returns>
        IDictionary<string, object> GetAll();


        /// <summary>
        /// 获取缓存尺寸
        /// </summary>
        /// <returns>缓存尺寸</returns>
        long Size();
    }
}
