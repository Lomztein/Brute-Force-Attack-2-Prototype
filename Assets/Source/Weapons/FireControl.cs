using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Weapons
{
    public abstract class FireControl : MonoBehaviour
    {
        public Action<int> FireCallback { get; set; }

        public abstract void Fire(float firerate);
    }
}

