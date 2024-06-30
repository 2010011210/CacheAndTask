using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Extension
{
    public static class CollectionExtension
    {

        //   collection:
        //
        //   pageSize:
        //
        //   action:
        //
        // 类型参数:
        //   T:
        public static void PageEach<T>(this ICollection<T> collection, int pageSize, Action<IList<T>> action)
        {
            collection.PageEach(pageSize, action, isParallel: false);
        }

        //
        // 摘要:
        //     根据集合分页循环处理每页数据
        //
        // 参数:
        //   collection:
        //
        //   pageSize:
        //
        //   action:
        //
        //   isParallel:
        //
        // 类型参数:
        //   T:
        public static void PageEach<T>(this ICollection<T> collection, int pageSize, Action<IList<T>> action, bool isParallel)
        {
            int pageIndex = 0;
            int recordCount = 0;
            int pageCount = 0;
            List<IList<T>> list = new List<IList<T>>();
            while (true)
            {
                IList<T> item = collection.PageList(pageSize, ref pageIndex, ref recordCount, out pageCount);
                if (recordCount <= 0)
                {
                    break;
                }

                list.Add(item);
                if (pageCount <= pageIndex)
                {
                    break;
                }

                pageIndex++;
            }

            if (isParallel && list.Count > 1)
            {
                try
                {
                    list.AsParallel().ForAll(action);
                }
                catch (AggregateException ex)
                {
                    using IEnumerator<Exception> enumerator = ex.InnerExceptions.GetEnumerator();
                    if (enumerator.MoveNext())
                    {
                        Exception current = enumerator.Current;
                        throw new ApplicationException("Task has exception.", current);
                    }
                }
            }
            else
            {
                list.ForEach(action);
            }
        }

        public static IList<T> PageList<T>(this ICollection<T> collection, int pageSize, ref int pageIndex, ref int recordCount, out int pageCount)
        {
            if (collection == null)
            {
                recordCount = 0;
                pageCount = 0;
                pageIndex = 0;
                return new List<T>(0);
            }

            recordCount = collection.Count;
            if (recordCount < 1)
            {
                pageCount = 0;
                pageIndex = 0;
                return new List<T>(0);
            }

            if (pageSize < 1)
            {
                pageSize = 1;
            }

            if (pageSize > recordCount)
            {
                pageSize = recordCount;
            }

            pageCount = Math.DivRem(recordCount, pageSize, out var result);
            if (result > 0)
            {
                pageCount++;
            }

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            if (pageIndex > 1)
            {
                return collection.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            }

            return collection.Take(pageSize).ToList();
        }

    }
}
