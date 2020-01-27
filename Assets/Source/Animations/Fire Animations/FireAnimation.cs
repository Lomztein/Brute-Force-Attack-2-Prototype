using Lomztein.BruteForceAttackSequel.Weapons;
using System.Collections;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Animation.Weapons
{
    public abstract class FireAnimation : MonoBehaviour
    {
        public Weapon Weapon;
        public SpriteRenderer AnimatedSprite;
        public Sprite DefaultSprite;
        public float PlaySpeedMultiplier = 1f;

        private void Awake()
        {
            Weapon.OnBeginFire += (info) => Play(1f / Weapon.Firerate);
        }

        public abstract void Play(float animSpeed);

        protected float GetAnimationDelay (Sprite[] sprites, float animationLength)
        {
            return animationLength / sprites.Length;
        }

        protected IEnumerator Animate(Sprite[] sprites, float delay)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                AnimatedSprite.sprite = sprites[i];
                yield return new WaitForSeconds(delay / PlaySpeedMultiplier);
            }
        }

        protected void ResetSprite ()
        {
            AnimatedSprite.sprite = DefaultSprite;
        }
    }
}
