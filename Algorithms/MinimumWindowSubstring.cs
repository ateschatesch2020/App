using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public static class MinimumWindowSubstring
    {
        public static string  MinWindow(string s, string t)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t))
                return "";

            Dictionary<char, int> need = new();
            Dictionary<char, int> window = new();

            // t içindeki karakter frekansları
            foreach (char c in t)
            {
                if (!need.ContainsKey(c))
                    need[c] = 0;

                need[c]++;
            }

            int have = 0;
            int needCount = need.Count;

            int left = 0;

            int minLen = int.MaxValue;
            int startIndex = 0;

            for (int right = 0; right < s.Length; right++)
            {
                char c = s[right];

                if (!window.ContainsKey(c))
                    window[c] = 0;

                window[c]++;

                // gerekli frekansa ulaştı mı?
                if (need.ContainsKey(c) &&
                    window[c] == need[c])
                {
                    have++;
                }

                // tüm karakterler tamamlandıysa
                while (have == needCount)
                {
                    int windowLen = right - left + 1;

                    if (windowLen < minLen)
                    {
                        minLen = windowLen;
                        startIndex = left;
                    }

                    char leftChar = s[left];

                    window[leftChar]--;

                    if (need.ContainsKey(leftChar) &&
                        window[leftChar] < need[leftChar])
                    {
                        have--;
                    }

                    left++;
                }
            }

            return minLen == int.MaxValue
                ? ""
                : s.Substring(startIndex, minLen);
        }
    }
}
