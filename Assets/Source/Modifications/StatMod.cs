using Lomztein.BruteForceAttackSequel.Modifiables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications
{
    [Serializable]
    public class StatMod : Mod
    {
        public enum StatType { Additive, Multiplicative }

        [ExposedProperty] public Stat.Type Identifier;
        [ExposedProperty] public StatType Type;
        [ExposedProperty] public ModProperty Value = new ModProperty();

        // This is a hack, remove some time in the future. Fix by adding a "Change-" type method to the Stat object.
        // Lol jk you're never gonna actually fix it.
        private IModdable _target;

        public override ModProperty[] Properties => new ModProperty[] { Coeffecient, Value };

        public StatMod (Tag modTag, Stat.Type identifier, StatType type, float value)
        {
            ModTag = modTag;
            Identifier = identifier;
            Type = type;
            StackIdentifier = Identifier.ToString();
            Value = new ModProperty (value, Type == StatType.Additive ? ModProperty.StackBehaviour.Add : ModProperty.StackBehaviour.Multiply);
            OnStackRecalculated += StatMod_OnStackRecalculated;
        }

        private void StatMod_OnStackRecalculated()
        {
            RemoveEffect(_target);
            ApplyEffect(_target);
        }

        public override void ApplyEffect(IModdable target)
        {
            _target = target;
            IStatContainer container = target as IStatContainer;
            Stat stat = container.GetStat(Identifier);

            //TODO: Consider having StatType be a member of Stat instead of StatMod, then handling how to add the stat in Stat.
            switch (Type)
            {
                case StatType.Additive:
                    stat.AddAdditive(Value.Get () * Coeffecient.Get (), this);
                    break;

                case StatType.Multiplicative:
                    stat.AddMultiplicative(Value.Get () * Coeffecient.Get (), this);
                    break;
            }
        }

        public override void RemoveEffect(IModdable target)
        {
            IStatContainer container = target as IStatContainer;
            Stat stat = container.GetStat(Identifier);

            switch (Type)
            {
                case StatType.Additive:
                    stat.RemoveAdditive(this);
                    break;

                case StatType.Multiplicative:
                    stat.RemoveMultiplicative(this);
                    break;
            }
        }
    }
}
