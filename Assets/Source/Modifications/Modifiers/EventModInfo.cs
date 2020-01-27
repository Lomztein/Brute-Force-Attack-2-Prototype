using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications.Modifiers
{
    [Serializable]
    public class EventModInfo : ModInfo
    {
        public string ModType;
        public string[] Properties;

        public override Mod GetMod ()
        {
            Mod mod = Activator.CreateInstance(Type.GetType(ModType)) as Mod;

            Type type = mod.GetType();
            FieldInfo[] fields = type.GetFields().Where(x => x.IsDefined(typeof(ExposedPropertyAttribute))).ToArray();

            if (Properties.Length != fields.Length)
            {
                throw new Exception("Properties array size is not the same as property fields in type " + type.Name);
            }

            for (int i = 0; i < Properties.Length; i++)
            {
                if (fields[i].FieldType == typeof (ModProperty))
                {
                    (fields[i].GetValue(mod) as ModProperty).Set ((float)StringConversion.FromString (Properties[i], typeof (float)));
                }
                else
                {
                    fields[i].SetValue(mod, StringConversion.FromString(Properties[i], fields[i].FieldType));
                }
            }

            return mod;
        }
    }
}
