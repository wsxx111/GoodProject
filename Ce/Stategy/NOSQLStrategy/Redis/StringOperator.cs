using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOSQLStrategy
{
    public class StringOperator : RedisOperatorBase
    {
        public StringOperator() : base() { }


        /// <summary>
        /// 设置
        /// </summary>
        public static void Set<T>(string key, T t)
        {
            string value = JsonSerializer.SerializeToString<T>(t);
            // Redis.Set<string>(key, t);  内部含有序列化
            Redis.SetEntry(key, value);
        }

        /// <summary>
        /// 设置键值
        /// </summary>
        public static bool SetKeyValue<T>(string key, T t)
        {
            try
            {
                return Redis.Set<T>(key, t);
            }
            catch (Exception)
            {
                throw;
            }         
        }

        /// <summary>
        ///不存在则设置
        /// </summary>
        public static bool SetIfNotExists<T>(string key, T t)
        {
            try
            {
                var value = JsonSerializer.SerializeToString<T>(t);
                return Redis.SetEntryIfNotExists(key, value);
            }
            catch (Exception)
            {               
                throw;
            }          
        }

        /// <summary>
        /// 获取
        /// </summary>
        public static T GetValueByKey<T>(string key)
        {
            return Redis.Get<T>(key);    //内部含有序列化                          
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool Exist(string key)
        {
            return Redis.ContainsKey(key);
        }


        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="key"></param>
        public static bool RemoveByKey(string key)
        {           
            return Redis.Remove(key); 
        } 


        /// <summary>
        /// 存储的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        ///  <returns>
        ///  None = 0,
        ///  String = 1,
        ///  List = 2,
        ///  Set = 3,
        ///  SortedSet = 4,
        ///  Hash = 5,      
        /// </returns>
        public static int GetType(string key)
        {
            return (int)Redis.GetEntryType(key);
        }

    }
}
