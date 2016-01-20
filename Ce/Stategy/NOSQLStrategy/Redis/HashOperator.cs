using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOSQLStrategy
{
    public class HashOperator : RedisOperatorBase
    {
        public HashOperator() : base() { }


        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        public static bool Exist<T>(string hashId, string key)
        {
            return Redis.HashContainsEntry(hashId, key);
        }


        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        public static bool Set<T>(string hashId, string key, T t)
        {
            var value = JsonSerializer.SerializeToString<T>(t);
            return Redis.SetEntryInHash(hashId, key, value);
        }

        /// <summary>
        /// 对于key未被使用的存储数据到hash表
        /// </summary>
        public static bool SetOnlyNotExists<T>(string hashId, string key, T t)
        {
            var value = JsonSerializer.SerializeToString<T>(t);
            return Redis.SetEntryInHashIfNotExists(hashId, key, value);
        }


        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        public static bool Remove(string hashId, string key)
        {
            return Redis.RemoveEntryFromHash(hashId, key);
        }


        /// <summary>
        /// 移除整个hash
        /// </summary>
        public static bool Remove(string key)
        {
            return Redis.Remove(key);
        }


        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        public static T Get<T>(string hashId, string key)
        {
            string value = Redis.GetValueFromHash(hashId, key);
            return JsonSerializer.DeserializeFromString<T>(value);
        }


        /// <summary>
        /// 获取整个hash的数据value
        /// </summary>
        public static List<T> GetAll<T>(string hashId)
        {
            var result = new List<T>();
            var list = Redis.GetHashValues(hashId);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    var value =JsonSerializer.DeserializeFromString<T>(item);
                    result.Add(value);
                }              
            }
            return result;
        }

        /// <summary>
        /// 获取整个hash的key数据
        /// </summary>
        public static List<string> GetAll()
        {
            var result = Redis.GetAllKeys();
            return result;
        }

        /// <summary>
        /// 获取键值数
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public static long GetCount(string hashId)
        {
            var count = Redis.GetHashCount(hashId);
            return count;
        }

        /// <summary>
        /// 将指定HashId的哈希表中的值加上指定值
        /// </summary>
        /// <returns></returns>
        public static long IncrNum(string hashId, string key, int num)
        {
            long newvalue = Redis.IncrementValueInHash(hashId, key, num);
            return newvalue;
        }

        /// <summary>
        /// 添加多个键值
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="hashlist"></param>
        /// <returns></returns>
        public static bool AddKeyValueList(string hashId, IEnumerable<KeyValuePair<string, string>> hashlist)
        {
            long OCount = Redis.GetHashCount(hashId);
            Redis.SetRangeInHash(hashId, hashlist);
            if (Redis.GetHashCount(hashId) == OCount + hashlist.ToArray().Length)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 添加对象T使hashmap多个键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashId"></param>
        /// <param name="t"></param>
        public static void AddKeyValueList<T>(string hashId, T t)
        {
            Dictionary<string, string> dlist = new Dictionary<string, string>();
            System.Reflection.PropertyInfo[] props = null;
            try
            {
                props = t.GetType().GetProperties();
                foreach (var item in props)
                {
                    dlist.Add(JsonSerializer.SerializeToString<string>(item.Name),JsonSerializer.SerializeToString<string>(item.GetValue(t, null).ToString()));
                }
                Redis.SetRangeInHash(hashId, dlist);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        public static void SetExpire(string key, DateTime datetime)
        {
            Redis.ExpireEntryAt(key, datetime);
        }


    }
}
