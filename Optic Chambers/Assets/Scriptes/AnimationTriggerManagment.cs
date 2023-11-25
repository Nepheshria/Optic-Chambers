using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerManagment : MonoBehaviour
{
    [SerializeField]private Animator _animator;
    private float lastHitTime;
    const float collisionInterval = 1000f;

    public void Update()
    {
        //Debug.Log(lastHitTime + " "+ Time.time + " "+collisionInterval);
        if (lastHitTime - Time.time < collisionInterval) //Time elapsed since last collision
            shutDown();
    }

    public void Hit()
    {
        lastHitTime = Time.time;
    }

    private void shutDown()
    {
        _animator.SetBool("Activated", false);
    }
}
