using Lomztein.BruteForceAttackSequel.Modifications.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Turrets
{
    [Serializable]
    public abstract class AttachmentPoint
    {
        public Vector2 Position;
        public float Angle;
        protected object _child;

        public bool IsFree() => _child == null;
    }

    [Serializable]
    public class ModifierAttachmentPoint : AttachmentPoint
    {
        public Modifier Modifier {
            get { return _child as Modifier; }
            set { _child = value; }
        }
    }

    [Serializable]
    public class ComponentAttachmentPoint : AttachmentPoint
    {
        public TurretComponent.Size Size;
        public TurretComponent Child {
            get { return _child as TurretComponent; }
            set { _child = value; }
        }
    }
}
