using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Enemies
{
    public class EnemySpawn : MonoBehaviour
    {
        public GameObject EnemyToSpawn;

        public float CurrentDelay;
        public float DelayDampening;
        public float DampenTime;

        public Vector2 SpawnCenter;
        public Vector2 SpawnSize;

        private void Start()
        {
            Invoke("Spawn", CurrentDelay);
            InvokeRepeating("DampenDelay", DampenTime, DampenTime);
        }

        private void Spawn ()
        {
            Vector2 position = SpawnCenter + new Vector2(
                Random.Range(-SpawnSize.x / 2f, SpawnSize.x / 2f),
                Random.Range(-SpawnSize.y / 2f, SpawnSize.y / 2f));

            GameObject newEnemy = Instantiate(EnemyToSpawn, position, Quaternion.identity, transform);

            Invoke("Spawn", CurrentDelay);
        }

        private void DampenDelay ()
        {
            CurrentDelay *= DelayDampening;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(SpawnCenter, SpawnSize);
        }
    }
}
