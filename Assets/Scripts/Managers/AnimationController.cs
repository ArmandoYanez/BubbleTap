using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    public AnimationClip animationClip;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void playAnimation()
    {
        gameObject.SetActive(true);
        animator.SetBool("Finish", true);
    }

    public void OnAnimationStart()
    {
        gameObject.SetActive(true);
    }

    public void OnAnimationEnd()
    {
        animator.SetBool("Finish", false);
        gameObject.SetActive(false);
    }
}
