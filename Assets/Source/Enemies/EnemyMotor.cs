using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Enemies
{
    public class EnemyMotor : MonoBehaviour
    {
        public Vector2 MoveDirection;
        public Rigidbody2D Rigidbody;

        private void FixedUpdate()
        {
            Rigidbody.MovePosition((Vector2)transform.position + MoveDirection * Time.fixedDeltaTime);
        }
    }
}