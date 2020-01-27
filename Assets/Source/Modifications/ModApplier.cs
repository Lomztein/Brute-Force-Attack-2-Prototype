using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Modifications
{
    public static class ModApplier
    {
        public static void ApplyMod (Mod mod, IModdable moddable)
        {
            mod.ApplyEffect(moddable);
        }

        public static void RemoveMod (Mod mod, IModdable moddable)
        {
            mod.RemoveEffect(moddable);
        }
    }
}
