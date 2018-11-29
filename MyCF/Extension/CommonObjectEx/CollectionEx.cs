//=====================================================
//Copyright (C) 2013-2015 All rights reserved
//CLR版本:      4.0.30319.42000
//命名空间:     MyCF.Extension.CommonObjectEx
//类名称:       CollectionEx
//创建时间:     2015/8/8 星期六 11:10:15
//作者：        Andy.Li
//作者站点:     http://www.cnblogs.com/lyandy
//======================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCF.Extension.CommonObjectEx
{
    public static class CollectionEx
    {
        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            return new[] { item };
        }

        public static IEnumerable<T> And<T>(this T item, T other)
        {
            return new[] { item, other };
        }

        public static IEnumerable<T> And<T>(this IEnumerable<T> items, T item)
        {
            foreach (var i in items)
            {
                yield return i;
            }
            yield return item;
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    action(item);
                }
            }
        }

        public static void LockAdd<T>(this List<T> list, T value)
        {
            lock (list)
            {
                list.Add(value);
            }
        }

        public static bool LockRemove<T>(this List<T> list, T value)
        {
            lock (list)
            {
                if (list.Contains(value))
                    return list.Remove(value);
                else return false;
            }
        }

        public static void LockAdd<T>(this ObservableCollection<T> list, T value)
        {
            lock (list)
            {
                list.Add(value);
            }
        }

        public static bool LockRemove<T>(this ObservableCollection<T> list, T value)
        {
            lock (list)
            {
                if (list.Contains(value))
                    return list.Remove(value);
                else return false;
            }
        }
    }
}
