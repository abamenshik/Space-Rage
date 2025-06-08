using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyCheats
{
    public static class Cheats
    {
        public static void Init()
        {
            List<CheatBase> cheatTypes = GetAllCheats();

            CreateGO(cheatTypes);
        }
        public static List<CheatBase> GetAllCheats()
        {
            List<CheatBase> cheatTypes = new();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(CheatBase)))
                    {
                        var cheat = Activator.CreateInstance(type) as CheatBase;
                        cheatTypes.Add(cheat);
                    }
                }
            }
            return cheatTypes;
        }
        private static void CreateGO(List<CheatBase> cheatTypes)
        {
            var go = new GameObject("Cheats");
            go.transform.parent = DynamicSpawn.DontDestroyOnLoadParent;
            var cheatsMono = go.AddComponent<CheatsMono>();
            cheatsMono.cheats = cheatTypes;
        }
    }
}