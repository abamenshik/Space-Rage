using System;
using UnityEngine;

namespace SpaceRage
{
    public static class ForEveryComponents
    {
        public static void For<T>(Func<T, bool> condtion, Action<T> doIt) where T : Component
        {
            var components = UnityEngine.Component.FindObjectsByType<T>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None);

            foreach (var item in components)
            {
                if (condtion(item))
                {
                    doIt(item);
                }
            }
        }
    }
}