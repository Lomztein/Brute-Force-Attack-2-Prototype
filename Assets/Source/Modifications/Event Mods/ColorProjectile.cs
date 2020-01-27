using Lomztein.BruteForceAttackSequel.Color.Colorizers;
using Lomztein.BruteForceAttackSequel.Modifiables;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications.EventMods
{
    public class ColorProjectile : EventMod<ProjectileEventInfo>
    {
        [ExposedProperty] public ModProperty Red = new ModProperty (0f, ModProperty.StackBehaviour.Add);
        [ExposedProperty] public ModProperty Green = new ModProperty(0f, ModProperty.StackBehaviour.Add);
        [ExposedProperty] public ModProperty Blue = new ModProperty(0f, ModProperty.StackBehaviour.Add);

        public override ModProperty[] Properties => new ModProperty[] { Red, Green, Blue };

        public override void Execute(object source, ProjectileEventInfo info)
        {
            float max = Mathf.Max(Red.Get(), Green.Get(), Blue.Get());
            UnityEngine.Color color = new UnityEngine.Color(Red.Get() / max, Green.Get() / max, Blue.Get() / max);
            info.Projectile.GetComponentInChildren<IColorizer>().SetColor(color);
        }
    }
}
