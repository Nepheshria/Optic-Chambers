using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    Vector3 difference = Vector3.zero;

    private void OnMouseDown()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }
}
