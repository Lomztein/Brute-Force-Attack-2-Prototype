using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Color
{
    public enum Color
    {
        Blue, Yellow, Red, Green, Purple
    }

    public static class ColorInfo
    {
        public static string[] Descriptions => new string[]
        {
            "Blue entities are simple and generic, setting the baseline for everything else.",
            "Yellow entities are light and fast, often capable of much higher speeds.",
            "Red entities are slow and heavy, quickly dominating anything with raw power.",
            "Green entities are healing and supportive, doing more for others than themselves.",
            "Purple entities are noumerous and swarming, overrunning everything."
        };

        public static UnityEngine.Color[] RGBValues => new UnityEngine.Color[]
        {
            new UnityEngine.Color (0f, 0f, 1f),
            new UnityEngine.Color (1f, 1f, 0f),
            new UnityEngine.Color (1f, 0f, 0f),
            new UnityEngine.Color (0f, 1f, 0f),
            new UnityEngine.Color (1f, 0f, 1f),
        };
    }
}
