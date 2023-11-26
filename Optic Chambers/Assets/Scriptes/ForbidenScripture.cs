using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForbidenScripture : MonoBehaviour
{
    public LayerMask Mask;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("ColIn");
        Vector2 ObjectCollided = other.transform.position;
        Vector2 exitDirection = ((Vector2)transform.position - ObjectCollided).normalized;

        other.transform.position = ObjectCollided + exitDirection * 2f;
    }
    
    private void OnCollisionStay(Collision other)
    {
        Debug.Log("ColStay");
        Vector2 ObjectCollided = other.transform.position;
        Vector2 exitDirection = ((Vector2)transform.position - ObjectCollided).normalized;

        other.transform.position = ObjectCollided + exitDirection * 2f;
    }
    
    private void OnCollisionExit(Collision other)
    {
        Debug.Log("ColOut");
        Vector2 ObjectCollided = other.transform.position;
        Vector2 exitDirection = ((Vector2)transform.position - ObjectCollided).normalized;

        other.transform.position = ObjectCollided + exitDirection * 2f;
    }
}
