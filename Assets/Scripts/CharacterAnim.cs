using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour, IAnimations
{
    SkeletonAnimation spine;

    private void Awake()
    {
        if (!TryGetComponent<SkeletonAnimation>(out spine))
        {
            Debug.LogError("SkeletonAnimation.cs 참조 실패 - ChatacterAnim.cs - Awake()");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            PlayMoveAnim();
            Debug.Log("pressed button");
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            spine.state.SetAnimation(0, "Idle", true);
        }
    }

    public void PlayMoveAnim()
    {
        Debug.Log("Animation is being played");
        spine.state.SetAnimation(0, "Move", true);      
    }
}
