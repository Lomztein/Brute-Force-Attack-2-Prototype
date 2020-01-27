using Lomztein.BruteForceAttackSequel.Modifiables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Modifications.Modifiers
{
    [Serializable]
    public class StatModInfo : ModInfo
    {
        public Mod.Tag ModTag;
        public Stat.Type Stat;
        public StatMod.StatType StatType;
        public float Value;

        public override Mod GetMod()
        {
            return new StatMod(ModTag, Stat, StatType, Value);
        }
    }
}
