using System.Collections;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Animation.Weapons
{
    public class TileableFireAnimation : FireAnimation
    {
        private bool _isPlaying;
        public Sprite[] StartAnimationSprites;
        public Sprite[] MidAnimationSprites;
        public Sprite[] EndAnimationSprites;

        private Coroutine _coroutine;

        public override void Play (float animSpeed)
        {
            if (!_isPlaying)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                float delay = GetAnimationDelay(MidAnimationSprites, animSpeed);
                _coroutine = StartCoroutine(StartAnimation(delay));
                _isPlaying = true;
            }
        }

        private IEnumerator StartAnimation(float delay)
        {
            yield return Animate(StartAnimationSprites, delay);
            yield return Switch(delay, Weapon.Firing);
        }

        private IEnumerator Switch (float delay, bool weaponStatus)
        {
            if (weaponStatus)
            {
                yield return Animate(MidAnimationSprites, delay);
                yield return Switch(delay, Weapon.Firing);
            }
            else
            {
                _isPlaying = false;
                yield return Animate(EndAnimationSprites, delay);
                ResetSprite();
            }
        }
    }
}
