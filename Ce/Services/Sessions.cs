﻿using Core;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Sessions
    {
        private static ISessionStrategy _isessionstrategy = null;//会话状态策略

        static Sessions()
        {
            _isessionstrategy = WebSessionData.Instance;
        }

        /// <summary>
        /// 生成sessionid
        /// </summary>
        /// <returns></returns>
        public static string GenerateSid()
        {
            long i = 1;
            byte[] byteArray = Guid.NewGuid().ToByteArray();
            foreach (byte b in byteArray)
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 过期时间(单位为秒)
        /// </summary>
        /// <value></value>
        public static int Timeout
        {
            get { return _isessionstrategy.Timeout; }
        }

        /// <summary>
        /// 获得用户会话状态数据
        /// </summary>
        /// <param name="sid">sid</param>
        /// <returns>Dictionary<string,object>类型</returns>
        public static Dictionary<string, object> GetSession(string sid)
        {
            return _isessionstrategy.GetSession(sid);
        }

        /// <summary>
        /// 移除用户会话状态数据
        /// </summary>
        /// <param name="sid">sid</param>
        public static void RemoverSession(string sid)
        {
            _isessionstrategy.RemoverSession(sid);
        }

        /// <summary>
        /// 获得用户会话状态数据的数据项的值
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <returns>当前键值不存在时返回null</returns>
        public static object GetValue(string sid, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return _isessionstrategy.GetValue(sid, key);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得用户会话状态数据的数据项的值
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static int GetValueInt(string sid, string key)
        {
            return TypeHelper.ObjectToInt(GetValue(sid, key));
        }

        /// <summary>
        /// 获得用户会话状态数据的数据项的值
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetValueString(string sid, string key)
        {
            object value = GetValue(sid, key);
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 设置用户会话状态数据的数据项
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetItem(string sid, string key, object value)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _isessionstrategy.SetItem(sid, key, value);
            }
        }

        /// <summary>
        /// 移除用户会话状态数据的数据项
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        public static void RemoveItem(string sid, string key)
        {
            if (!string.IsNullOrEmpty(key))
                _isessionstrategy.RemoveItem(sid, key);
        }

    }
}
