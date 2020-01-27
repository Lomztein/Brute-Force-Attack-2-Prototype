using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Modifications
{
    public interface IModContainer
    {
        bool AddMod(Mod mod);

        bool RemoveMod(Mod mod);

        Mod[] GetMods();

        Mod[] GetUniqueMods();
    }
}
