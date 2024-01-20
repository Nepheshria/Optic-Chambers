using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public int timer;
    
    // Update is called once per frame
    void Update()
    {
        if (timer > 30)
        {
            _animator.SetBool("Contact", false);
        }

        timer++;
    }
}
