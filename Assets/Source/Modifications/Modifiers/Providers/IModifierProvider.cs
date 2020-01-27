using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Modifications.Modifiers.Providers
{
    public interface IModifierProvider
    {
        void Apply();

        void Remove();

        event Action<IModdable> OnApplied;
        event Action<IModdable> OnRemoved;
    }
}
