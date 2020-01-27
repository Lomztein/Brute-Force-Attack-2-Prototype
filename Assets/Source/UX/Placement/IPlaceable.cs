using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Placement
{
    public interface IPlacement
    {
        bool ToPosition(Vector2 position, Quaternion rotation);

        bool ToTransforms(Transform[] transforms);

        bool Place();

        bool PickUp(GameObject obj);
    }
}