using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel
{
    public static class World
    {
        public static Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
