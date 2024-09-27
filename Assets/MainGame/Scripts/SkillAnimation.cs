using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        if(!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("Animator 참조 실패 - SkillAnimation.cs - Awake()");
        }  
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(PlayAnimation());
        }
    }
    private IEnumerator PlayAnimation()
    {
        int i = 0;
        while (i < 5)
        {
            anim.SetTrigger("SkillCast");           
            yield return new WaitForSeconds(1f); 
            transform.position += new Vector3(2.0f, 0, 0); 
            i++;
        }
    }

}
