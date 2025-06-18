using UnityEngine;

namespace Scripts.Extension
{
    public static class StringExtension
    {
        public static T Load<T>(this string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }
    }
}