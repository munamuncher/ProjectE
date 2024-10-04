using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAnim : MonoBehaviour, IAnimations
{
    private SkeletonAnimation spine;

    private void Awake()
    {
        if (!TryGetComponent<SkeletonAnimation>(out spine))
        {
            Debug.LogError("SkeletonAnimation.cs 참조 실패 - ChatacterAnim.cs - Awake()");
        }
        spine.AnimationState.End += HandleSpineEvent;
    }
    private void HandleSpineEvent(Spine.TrackEntry trackEntry)
    {
        if(trackEntry.Animation.Name == "Attack")
        {
            Debug.Log("im doing Damage");
        }
    }
    public void PlayAnim(string animationName ,bool loop)
    {
           spine.state.SetAnimation(0, animationName, loop);
            Debug.Log("animation name  " + animationName);
    }
}
