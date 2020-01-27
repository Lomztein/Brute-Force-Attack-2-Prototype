using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Modifications
{
    public static class ModContainerExtensions
    {
        public static T[] GetModsOfType<T> (this IModContainer container) where T : Mod {
            return (T[])container.GetMods().Where(x => x is T).ToArray();
        }

        public static Mod[] GetModsOfType(this IModContainer container, Type type)
        {
            return container.GetMods().Where(x => type.IsInstanceOfType (x)).ToArray();
        }

        public static Mod[] GetModsOfIdentifier (this IModContainer container, string identifier)
        {
            return container.GetMods().Where(x => x.StackIdentifier == identifier).ToArray();
        }

        public static T GetUniqueModOfType<T> (this IModContainer container) where T : Mod
        {
            return (T)container.GetUniqueMods().FirstOrDefault(x => x is T);
        }

        public static Mod GetUniqueModOfType(this IModContainer container, Type type)
        {
            return container.GetUniqueMods().FirstOrDefault(x => type.IsInstanceOfType (x));
        }

        public static Mod GetUniqueModOfIdentifier (this IModContainer container, string identifier)
        {
            return container.GetUniqueMods().FirstOrDefault(x => x.StackIdentifier == identifier);
        }

        public static void RecalculateStack(this IModContainer container, string identifier)
        {
            Mod[] nonUnique = container.GetModsOfIdentifier(identifier);
            container.GetUniqueModOfIdentifier(identifier).StackWith(nonUnique);
        }


    }
}
