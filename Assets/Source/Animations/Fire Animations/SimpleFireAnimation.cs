using System.Collections;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Animation.Weapons
{
    public class SimpleFireAnimation : FireAnimation
    {
        public Sprite[] AnimationSprites;

        public override void Play (float animSpeed)
        {
            StopAllCoroutines();
            StartCoroutine(StartAnimation(GetAnimationDelay(AnimationSprites, animSpeed)));
        }

        private IEnumerator StartAnimation (float delay)
        {
            yield return Animate(AnimationSprites, delay);
            ResetSprite();
        }
    }
}
