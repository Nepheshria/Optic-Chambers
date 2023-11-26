using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Color buttonColor;
    [SerializeField] private DoorManager door;
    [SerializeField] private Animator _animator;
    
    // Timing
    private float lastHitTime;
    const float collisionInterval = 5f;

    private bool isActive;
    private bool lastState;
    
    
    public bool isRightColor(LaserObject enteringLaser)
    {
        return enteringLaser.getColor() != buttonColor;
    }

    public void Activate()
    {
        lastHitTime = 0;
        isActive = true;
        _animator.SetBool(Activated, true);
    }

    private void Update()
    {
        lastHitTime++;
        if (lastHitTime >= collisionInterval)
        {
            isActive = false;
        }

        if (isActive != lastState)
        {
            door.changeDoorState();
        }

        lastState = isActive;

    }
}
