using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Projectiles.HitscanRenderers
{
    public abstract class HitscanRenderer : MonoBehaviour
    {
        abstract public void SetPositions(Vector3 start, Vector3 end);
    }
}
