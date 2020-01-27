using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel
{
    public static class StringConversion
    {
        private static Converter[] _converters = new Converter[]
        {
            new Converter (typeof (int), x => int.Parse (x), x => x.ToString ()),
            new Converter (typeof (string), x => x, x => x.ToString ()),
            new Converter (typeof (float), x => float.Parse (x), x => x.ToString ()),
            new Converter (typeof (Resource), x => new Resource (x), x => x.ToString ()),
            new Converter (typeof (bool), x => bool.Parse (x), x => x.ToString ()),
        };

        public static object FromString (string value, Type targetType)
        {
            return GetConverter(targetType).FromString(value);
        }

        public static string ToString (object value)
        {
            Type type = value.GetType();
            return GetConverter(type).ToString(value);
        }

        private static Converter GetConverter (Type type)
        {
            if (type.IsEnum)
            {
                return new Converter(type, x =>
               {
                   Enum val = null;
                   foreach (var enumValue in Enum.GetValues(type))
                   {
                       if (enumValue.ToString() == x)
                       {
                           val = enumValue as Enum;
                       }
                   }
                   return val;
               }, x => x.ToString());
            }

            Converter converter = _converters.FirstOrDefault(x => x.TargetType.IsEquivalentTo(type) || x.TargetType.IsSubclassOf (type));
            if (converter == null)
                throw new InvalidOperationException("A converter for type " + type.Name + " doesn't exist.");
            return converter;
        }

        private class Converter
        {
            public Converter (Type targetType, Func<string, object> from, Func<object, string> to)
            {
                TargetType = targetType;
                FromString = from;
                ToString = to;
            }

            public readonly Type TargetType;

            public readonly Func<string, object> FromString;
            public readonly new Func<object, string> ToString;
        }
    }
}
