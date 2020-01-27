using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifiables
{
    public class Event
    {
        public enum Type
        {
            /* Damage events */ OnHit, OnFirstHit, OnPierce, OnLastHit, OnAOEHit, OnDOTHit,
            /* Target events */ OnTargetAquired, OnTargetLost, OnTargetOutOfRange,
            /* Weapon events */ OnBeginFire, OnFire, OnProjectileFired, OnProjectileFinished,
        }

        public delegate void EventHandler(object sender, EventInfo info);

        public event EventHandler ThisEvent;

        public void Raise(object sender, EventInfo info) => ThisEvent?.Invoke(sender, info);

        public void Attach (EventHandler handler)
        {
            ThisEvent += handler;
        }

        public void Detach (EventHandler handler)
        {
            ThisEvent -= handler;
        }
    }
}