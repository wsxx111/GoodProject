using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SysCacheData
    {
        private static object _locker = new object();//锁对象
        private static ICacheStrategy _icachestrategy = null;//缓存策略
        private static ICacheManager _icachemanager = null;//缓存管理

        static SysCacheData()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "CacheStrategy.dll", SearchOption.TopDirectoryOnly);
                if (fileNameList.Length > 0)
                {
                    _icachestrategy = (ICacheStrategy)Activator.CreateInstance(Type.GetType("CacheStrategy.CacheStrategy, CacheStrategy"));
                }
                else
                {
                    throw new Exception("创建'缓存策略对象'失败,未在bin目录中中找到'缓存策略程序集‘CacheStrategy'");
                }
            }
            catch
            {
                throw new Exception("创建'缓存策略对象'失败,创建'缓存策略程序集‘CacheStrategy'时发生异常");
            }
            _icachemanager = new CacheByRegex();
        }

        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        public static object Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }
            return _icachestrategy.Get(_icachemanager.GenerateGetKey(key));
        }

        /// <summary>
        /// 缓存过期时间
        /// </summary>
        public static int TimeOut
        {
            get
            {
                return _icachestrategy.TimeOut;
            }
            set
            {
                lock (_locker)
                {
                    _icachestrategy.TimeOut = value;
                }
            }
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        public static void Insert(string key, object data)
        {
            if (string.IsNullOrWhiteSpace(key) || data == null)
            {
                return;
            }
            lock (_locker)
            {
                _icachestrategy.Insert(_icachemanager.GenerateInsertKey(key), data);
            }
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中,并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        public static void Insert(string key, object data, int cacheTime)
        {
            if (string.IsNullOrWhiteSpace(key) || data == null)
            {
                return;
            }
            lock (_locker)
            {
                _icachestrategy.Insert(_icachemanager.GenerateInsertKey(key), data, cacheTime);
            }
        }

        /// <summary>
        /// 从缓存中移除指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        public static void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;
            lock (_locker)
            {
                foreach (string k in _icachemanager.GenerateRemoveKey(key))
                    _icachestrategy.Remove(k);
            }
        }

        /// <summary>
        /// 清空缓存所有对象
        /// </summary>
        public static void Clear()
        {
            lock (_locker)
            {
                _icachestrategy.Clear();
            }
        }

    }
}
