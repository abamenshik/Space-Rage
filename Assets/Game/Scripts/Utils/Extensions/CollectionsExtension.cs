using System.Collections;
using System.Collections.Generic;

namespace Extension
{
    public static class CollectionsExtension
    {
        //public static bool IsInited(this ICollection collection)
        //{
        //    return collection != null && collection.Count > 0;
        //}
        public static bool IsInited<T>(this ICollection<T> collection)
        {
            return collection != null && collection.Count > 0;
        }
    }
    public static class ListExtension
    {
        // add shuffle method
        public static T Pop<T>(this IList<T> list)
        {
            var lastIndex = list.Count - 1;
            var item = Extract(list, lastIndex);

            return item;
        }
        public static T Extract<T>(this IList<T> list, int index)
        {
            if (!list.IsInited())
            {
                throw new System.Exception("Cannot extract from an empty list.");
            }

            T item = list[index];
            list.RemoveAt(index);

            return item;
        }
    }
}
