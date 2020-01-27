using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Modifications
{
    public interface IModdable
    {
        Mod.Tag[] GetModTags();

        void AddTag(Mod.Tag tag);
        void RemoveTag(Mod.Tag tag);

        bool AddMod(Mod mod);
        bool RemoveMod(Mod mod);
    }
}
