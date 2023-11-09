using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : Singleton<AnimationController>
{
    Animator rocketAnimator;

    enum AnimationStatus
    {
        Bullet = 0,
        Rocket = 1,
        //....

    }
    void Start()
    {
        rocketAnimator = GetComponent<Animator>();
    }

    void Update()
    {/*
        rocketAnimator.SetBool("isShootingRocket", true);*/
    }
}
