using System.Collections.Generic;

namespace MyCheats
{
    public static class CharExtension
    {
        private static Dictionary<char, char> rusToEng = new()
        {
            {'é','q' },
            {'ö','w' },
            {'ó','e' },
            {'ê','r' },
            {'å','t' },
            {'í','y' },
            {'ã','u' },
            {'ø','i' },
            {'ù','o' },
            {'ç','p' },
            {'ô','a' },
            {'û','s' },
            {'â','d' },
            {'à','f' },
            {'ï','g' },
            {'ð','h' },
            {'î','j' },
            {'ë','k' },
            {'ä','l' },
            {'ÿ','z' },
            {'÷','x' },
            {'ñ','c' },
            {'ì','v' },
            {'è','b' },
            {'ò','n' },
            {'ü','m' }
        };
        public static char RusToEngKeyboardLayout(this char ch)
        {
            return rusToEng.ContainsKey(ch) ? rusToEng[ch] : ch;
        }
    }
}