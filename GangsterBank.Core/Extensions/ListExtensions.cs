namespace GangsterBank.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class ListExtensions
    {
        public static ReadOnlyCollection<T> AsSafeReadOnly<T>(this List<T> list)
        {
            if (list == null)
                return (ReadOnlyCollection<T>)null;
            else
                return list.AsReadOnly();
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (list == null || action == null)
                return;
            foreach (T obj in list)
                action(obj);
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T, int> action)
        {
            if (list == null || action == null)
                return;
            int num = 0;
            foreach (T obj in list)
            {
                action(obj, num);
                ++num;
            }
        }

        public static void ForEach<T>(this IList<T> list, Action<T, bool> action)
        {
            if (list == null || action == null)
                return;
            int num = 0;
            foreach (T obj in (IEnumerable<T>)list)
            {
                action(obj, num >= list.Count - 1);
                ++num;
            }
        }

        public static ReadOnlyCollection<T> ToSafeReadOnlyCollection<T>(this IEnumerable<T> enumerable) where T : class
        {
            if (enumerable == null)
                return (ReadOnlyCollection<T>)null;
            else
                return new List<T>(Enumerable.Where<T>(enumerable, (Func<T, bool>)(_ => (object)_ != null))).AsReadOnly();
        }
    }
}
