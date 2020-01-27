using Lomztein.BruteForceAttackSequel.Projectiles;
using Lomztein.BruteForceAttackSequel.Damage;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Lomztein.BruteForceAttackSequel.Modifiables;

namespace Lomztein.BruteForceAttackSequel.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public GameObject Projectile;
        private Projectile _projectileInfo;

        public Transform[] Muzzles;
        public ParticleSystem[] MuzzleFlashes;
        public int MuzzleParticles;
        public LayerMask LayerMask;

        public float Firerate { get; private set; }
        public float Pricision { get; private set; }
        public float Damage { get; private set; }
        public float Amount { get; private set; }
        public float Range { get; private set; }

        public event Action<DamageInfo> OnHit;
        public event Action<DamageInfo> OnFirstHit;
        public event Action<DamageInfo> OnPierce;
        public event Action<DamageInfo> OnLastHit;
        public event Action<DamageInfo> OnAOEHit;
        public event Action<DamageInfo> OnDOTHit;

        public event Action<FireEventInfo> OnFire;
        public event Action<FireEventInfo> OnBeginFire;
        public event Action<ProjectileEventInfo> OnProjectileFired;
        public event Action<ProjectileEventInfo> OnProjectileFinished;

        public bool Firing { get; set; }
        public Transform Target { get; set; }

        private bool _loaded = true;

        [SerializeField] private FireControl _fireControl;

        private void Awake()
        {
            _fireControl.FireCallback = new Action<int>(x => FireProjectile(x));
            _projectileInfo = Projectile.GetComponent<Projectile>();
        }

        public float GetProjectileSpeed ()
        {
            return _projectileInfo.GetSpeed();
        }

        public void SetFirerate (float value)
        {
            Firerate = value;
            Debug.Log("Setting firerate!");
        }

        public void SetPrecision (float value)
        {
            Pricision = value;
        }

        public void SetDamage (float value)
        {
            Damage = value;
        }

        public void SetProjectileMul (float value)
        {
            Amount = value;
        }

        public void SetRange (float value)
        {
            Range = value;
        }

        private float GetFireDelay ()
        {
            return 1 / Firerate;
        }

        private bool CanFire ()
        {
            return _loaded;
        }

        public bool Fire()
        {
            if (CanFire ())
            {
                OnBeginFire?.Invoke(new FireEventInfo (Muzzles[0], this));
                _fireControl.Fire(GetFireDelay ());
                StartCoroutine(Rechamber());
                return true;
            }
            return false;
        }

        private void FixedUpdate () {
            if (Firing)
            {
                Fire();
            }
        }

        private IEnumerator Rechamber ()
        {
            _loaded = false;
            yield return new WaitForSeconds(GetFireDelay ());
            _loaded = true;
        }

        private void FireProjectile (int index)
        {
            Transform muzzle = Muzzles[index % Muzzles.Length];
            int amount = Mathf.RoundToInt (Amount);
            for (int i = 0; i < amount; i++)
            {
                GameObject newProjectile = Instantiate(Projectile, muzzle.position, muzzle.rotation);
                Projectile projectileScript = newProjectile.GetComponent<Projectile>();

                float rad = Pricision * Mathf.Deg2Rad;
                Vector2 direction = muzzle.right + muzzle.rotation * (Vector3.up * Mathf.Sin(UnityEngine.Random.Range(-rad, rad)));

                projectileScript.Fire(this, direction, LayerMask, Target, Damage, Range);
                AttachProjectileCallbacks(projectileScript);

                OnProjectileFired?.Invoke(new ProjectileEventInfo(projectileScript));
            }
            OnFire?.Invoke(new FireEventInfo (muzzle, this));
        }

        public void AttachProjectileCallbacks (Projectile proj)
        {
            proj.AttachEvents(
                new Action<DamageInfo>((info) => OnHit?.Invoke(info)),
                new Action<DamageInfo>((info) => OnFirstHit?.Invoke(info)),
                new Action<DamageInfo>((info) => OnPierce?.Invoke(info)),
                new Action<DamageInfo>((info) => OnLastHit?.Invoke(info)),
                new Action<DamageInfo>((info) => OnAOEHit?.Invoke(info)),
                new Action<DamageInfo>((info) => OnDOTHit?.Invoke(info)),
            new Action<Projectile>((info) => OnProjectileFinished(new ProjectileEventInfo(info))));
        }
    }
}
