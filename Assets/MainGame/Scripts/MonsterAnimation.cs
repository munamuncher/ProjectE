using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour, IAnimations
{
    private Animator anim;
    private Coroutine currentAnimationCoroutine;

    private void Awake()
    {
        if (!TryGetComponent(out anim))
        {
            Debug.LogError("Animator 참조 실패 - MonsterAnimation.cs - Awake()");
        }
    }

    public void PlayAnim(string animationName, bool loop)
    {
        if (currentAnimationCoroutine != null)
        {
            StopCoroutine(currentAnimationCoroutine);
            currentAnimationCoroutine = null;
        }

        switch (animationName)
        {
            case "attack":
                currentAnimationCoroutine = StartCoroutine(PlayAnimationLoop("attack", loop));
                break;
            case "idle":
                anim.SetBool("isMoving", false);
                break;
            case "move":
                anim.SetBool("isMoving", true);
                break;
            case "hit":
                anim.SetTrigger("hit");
                break;
        }
    }

    IEnumerator PlayAnimationLoop(string animationName, bool loop)
    {
        do
        {
            anim.SetTrigger(animationName);
            Debug.Log($"Playing animation: {animationName}");
            yield return new WaitForSeconds(1f);
        }
        while (loop);
    }
}
