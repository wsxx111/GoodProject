using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOSQLStrategy
{
    public class SortSet : RedisOperatorBase
    {
        public SortSet() : base() { }


        /// <summary>
        /// 向有序集合中添加元素
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        public static void AddItemToSortedSet(string set, string value, long score)
        {
            Redis.AddItemToSortedSet(set, value, score);
        }
        /// <summary>
        /// 获得某个值在有序集合中的排名，按分数的降序排列
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long GetItemIndexInSortedSetDesc(string set, string value)
        {
            long index = Redis.GetItemIndexInSortedSetDesc(set, value);
            return index;
        }
        /// <summary>
        /// 获得某个值在有序集合中的排名，按分数的升序排列
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long GetItemIndexInSortedSet(string set, string value)
        {
            long index = Redis.GetItemIndexInSortedSet(set, value);
            return index;
        }
        /// <summary>
        /// 获得有序集合中某个值得分数
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double GetItemScoreInSortedSet(string set, string value)
        {
            double score = Redis.GetItemScoreInSortedSet(set, value);
            return score;
        }
        /// <summary>
        /// 获得有序集合中，某个排名范围的所有值
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginRank"></param>
        /// <param name="endRank"></param>
        /// <returns></returns>
        public static List<string> GetRangeFromSortedSet(string set, int beginRank, int endRank)
        {
            List<string> valueList = Redis.GetRangeFromSortedSet(set, beginRank, endRank);
            return valueList;
        }
        /// <summary>
        /// 获得有序集合中，某个分数范围内的所有值，升序
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginScore"></param>
        /// <param name="endScore"></param>
        /// <returns></returns>
        public static List<string> GetRangeFromSortedSet(string set, double beginScore, double endScore)
        {
            List<string> valueList = Redis.GetRangeFromSortedSetByHighestScore(set, beginScore, endScore);
            return valueList;
        }
        /// <summary>
        /// 获得有序集合中，某个分数范围内的所有值，降序
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginScore"></param>
        /// <param name="endScore"></param>
        /// <returns></returns>
        public static List<string> GetRangeFromSortedSetDesc(string set, double beginScore, double endScore)
        {
            List<string> vlaueList = Redis.GetRangeFromSortedSetByLowestScore(set, beginScore, endScore);
            return vlaueList;
        }       

    }
}
