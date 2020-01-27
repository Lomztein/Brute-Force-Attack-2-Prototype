using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Experimental.Enemies
{
    public class EnemySystem : ComponentSystem
    {
        struct Components
        {
            public Health health;
            public EnemyMotor motor;
        }

        protected override void OnUpdate()
        {
        }
    }
}