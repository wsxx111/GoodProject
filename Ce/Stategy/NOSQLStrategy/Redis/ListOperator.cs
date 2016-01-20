using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOSQLStrategy
{
    public class ListOperator : RedisOperatorBase
    {
        public ListOperator() : base() { }


        /// <summary>
        ///添加
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="list"></param>
        public static void AddItemToListList<T>(string listId, T t)
        {
            var redisTypedClient = Redis.GetTypedClient<T>();
            redisTypedClient.AddItemToList(redisTypedClient.Lists[listId], t);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <param name="values"></param>
        public static void AddRangeItemToList<T>(string listId, List<T> items)
        {           
            foreach (var it in items)
            {
                AddItemToListList<T>(listId,it);
            }           
        }
      

        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool List_RemoveBylistId<T>(string listId, T t)
        {
            var redisTypedClient = Redis.GetTypedClient<T>();
            return redisTypedClient.RemoveItemFromList(redisTypedClient.Lists[listId], t) > 0;
        }

        /// <summary>
        /// 移除所有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public static void List_RemoveAllBylistId<T>(string listId)
        {
            var redisTypedClient = Redis.GetTypedClient<T>();
            redisTypedClient.Lists[listId].RemoveAll();
        }

        /// <summary>
        /// 获得个数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long List_CountBylistId(string listId)
        {
            return Redis.GetListCount(listId);
        }

        /// <summary>
        /// 获得链表某范围的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<T> List_GetRange<T>(string listId, int start, int count)
        {
            var c = Redis.GetTypedClient<T>();
            return c.Lists[listId].GetRange(start, start + count - 1);
        }


        /// <summary>
        /// 链表 分页获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pageIndex">那一页</param>
        /// <param name="pageSize">每页的个数</param>
        /// <returns></returns>
        public static List<T> List_GetList<T>(string listId, int pageIndex, int pageSize)
        {
            int start = pageSize * (pageIndex - 1);
            return List_GetRange<T>(listId, start, pageSize);
        }

        /// <summary>
        /// 获得链表所有的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> List_GetList<T>(string listId)
        {
            var c = Redis.GetTypedClient<T>();
            return c.Lists[listId].GetRange(0, c.Lists[listId].Count);
        }      

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public static void List_SetExpire(string listId, DateTime datetime)
        {
            Redis.ExpireEntryAt(listId, datetime);
        }
      

        /// <summary>
        /// 从list类型数据指定索引处获取数据，支持正索引和负索引
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T GetItemFromList<T>(string listId, int index)
        {
            string item = Redis.GetItemFromList(listId, index);
            return JsonSerializer.DeserializeFromString<T>(item);
        }

    }
}
