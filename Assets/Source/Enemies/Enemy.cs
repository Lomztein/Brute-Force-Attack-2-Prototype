using Lomztein.BruteForceAttackSequel.Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float _health;
    [SerializeField] private float _lifeTime;
    private bool _isDead = false;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    public float TakeDamage(DamageInfo damage)
    {
        float toTake = Mathf.Min(damage.Amount, _health);
        _health -= toTake;

        if (_health <= 0)
        {
            Die();
        }

        return toTake;
    }

    private void Die ()
    {
        Destroy(gameObject);
    }
}
