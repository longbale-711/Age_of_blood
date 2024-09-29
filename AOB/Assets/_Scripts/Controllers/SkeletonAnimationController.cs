using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationController : MonoBehaviour
{
    private const string DIE = "Die";

    [SerializeField] private Animator _animator;

    public void PlayDyingAnim(Action onAnimCompleted = null)
    {
        _animator.SetTrigger(DIE);
        WaitForAnimationToEnd(DIE,onAnimCompleted).Forget();

    }

    private async UniTaskVoid WaitForAnimationToEnd(string animationName, Action onAnimCompleted = null)
    {
        // Wait until animation start
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        while (!stateInfo.IsName(animationName))
        {
            // Wait for the next frame
            await UniTask.Yield();
            stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        }

        // Wait until animation end
        while (stateInfo.normalizedTime < 1f)
        {
            await UniTask.Yield();
            stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        }

        // Animation finished
        onAnimCompleted?.Invoke();
    }

}
