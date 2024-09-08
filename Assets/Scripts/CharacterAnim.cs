using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAnim : MonoBehaviour, IAnimations
{
    SkeletonAnimation spine;

    private void Awake()
    {
        if (!TryGetComponent<SkeletonAnimation>(out spine))
        {
            Debug.LogError("SkeletonAnimation.cs ���� ���� - ChatacterAnim.cs - Awake()");
        }
    }

    public void PlayMoveAnim(string animationName)
    {
           spine.state.SetAnimation(0, animationName, true);
            Debug.Log("animation name  " + animationName);
    }
}
