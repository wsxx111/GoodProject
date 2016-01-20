using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOSQLStrategy
{
    public class SetOperator : RedisOperatorBase
    {
        public SetOperator() : base() { }   

        /// <summary>
        /// 向集合中添加数据
        /// </summary>
        /// <param name="item"></param>
        /// <param name="set"></param>
        public static void AddItemToSet<T>(string setId, T t)
        {
            var redisTypedClient = Redis.GetTypedClient<T>();
            redisTypedClient.Sets[setId].Add(t);            
        }

        /// <summary>
        /// 添加多项到集合中
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="items"></param>
        public static void AddRangeItemToSet<T>(string setId, List<T> items)
        {
            foreach(var t in items)
            {
                AddItemToSet<T>(setId, t);
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool ContainsSet<T>(string key, T t)
        {         
                var redisTypedClient = Redis.GetTypedClient<T>();
                return redisTypedClient.Sets[key].Contains(t);   
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool RemoveSet<T>(string key, T t)
        {           
                var redisTypedClient = Redis.GetTypedClient<T>();
                return redisTypedClient.Sets[key].Remove(t);  
        } 

        /// <summary>
        /// 移除某集合中某值
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        public static void RemoveItemFromSet(string setId, string item)
        {
            Redis.RemoveItemFromSet(setId, item);
        }

        /// <summary>
        /// 获得集合中所有数据
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static HashSet<string> GetAllItemsFromSet(string SetId)
        {
            HashSet<string> items = Redis.GetAllItemsFromSet(SetId);
            return items;
        }
        /// <summary>
        /// 获取fromSet集合和其他集合不同的数据
        /// </summary>
        /// <param name="fromSet"></param>
        /// <param name="toSet"></param>
        /// <returns></returns>
        public static HashSet<string> GetSetDiff(string fromSetId, params string[] toSetId)
        {
            HashSet<string> diff = Redis.GetDifferencesFromSet(fromSetId, toSetId);
            return diff;
        }
        /// <summary>
        /// 获得所有集合的并集
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static HashSet<string> GetSetUnion(params string[] setIds)
        {
            HashSet<string> union = Redis.GetUnionFromSets(setIds);
            return union;
        }
        /// <summary>
        /// 获得所有集合的交集
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static HashSet<string> GetSetInter(params string[] set)
        {
            HashSet<string> inter = Redis.GetIntersectFromSets(set);
            return inter;
        }

    }
}
