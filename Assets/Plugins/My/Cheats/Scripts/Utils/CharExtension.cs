using System.Collections.Generic;

namespace MyCheats
{
    public static class CharExtension
    {
        private static Dictionary<char, char> rusToEng = new()
        {
            {'�','q' },
            {'�','w' },
            {'�','e' },
            {'�','r' },
            {'�','t' },
            {'�','y' },
            {'�','u' },
            {'�','i' },
            {'�','o' },
            {'�','p' },
            {'�','a' },
            {'�','s' },
            {'�','d' },
            {'�','f' },
            {'�','g' },
            {'�','h' },
            {'�','j' },
            {'�','k' },
            {'�','l' },
            {'�','z' },
            {'�','x' },
            {'�','c' },
            {'�','v' },
            {'�','b' },
            {'�','n' },
            {'�','m' }
        };
        public static char RusToEngKeyboardLayout(this char ch)
        {
            return rusToEng.ContainsKey(ch) ? rusToEng[ch] : ch;
        }
    }
}