using Core.Helper;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NOSQLStrategy
{
    public abstract class RedisOperatorBase : IDisposable
    {
        public static IRedisClient Redis { get; private set; }
        private bool _disposed = false;
        static RedisOperatorBase()
        {         
            if (Redis==null)
            {
                Redis = RedisHelper.GetClient();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Redis.Dispose();
                    Redis = null;
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            Redis.Save();
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            Redis.SaveAsync();
        }
    }    
}
