using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAnim : MonoBehaviour, IAnimations
{
    SkeletonAnimation spine;

    public bool playAnimation = false;

    private void Awake()
    {
        if (!TryGetComponent<SkeletonAnimation>(out spine))
        {
            Debug.LogError("SkeletonAnimation.cs 참조 실패 - ChatacterAnim.cs - Awake()");
        }
    }

    public void PlayMoveAnim(string animationName)
    {
        if (!playAnimation)
        {
            spine.state.SetAnimation(0, animationName, true);
        }
        else
        {
            Debug.Log("animation name" + animationName);
            Debug.Log("Animation is being played");
            spine.state.SetAnimation(0, animationName, true);
        }
    }
}
