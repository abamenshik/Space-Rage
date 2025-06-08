using System.Text;

namespace MyCheats
{
    public static class CheatsMenuItem
    {
        [UnityEditor.MenuItem("Tools/Cheats/Print All")]
        public static void PrintAllCheats()
        {
            var all = Cheats.GetAllCheats();

            StringBuilder sb = new StringBuilder();

            foreach (var cheat in all)
            {
                sb.Append(cheat.Name);
                sb.Append(" - ");
                sb.Append(cheat.Description);
                sb.AppendLine();
            }


            UnityEngine.Debug.Log(sb.ToString());
        }
    }
}
