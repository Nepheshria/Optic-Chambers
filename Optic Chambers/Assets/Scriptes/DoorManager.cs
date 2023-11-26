using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    private bool curentState; // False Cloosed / True Open

    private Vector2 openPosition;
    private Vector2 closedPosition;
    private Vector2 targetPos;
    private GameObject doorBlock;

    public void changeDoorState()
    {
        curentState = !curentState;
        if (curentState)
        {// Open
            Open();
        }
        else
        { // Close
            Close();
        }
        
    }

    private void Open()
    {
        Debug.Log("Opening Door");
        targetPos = openPosition;
    }
    
    private void Close()
    {
        Debug.Log("Closing Door");
        targetPos = closedPosition;
    }

    private void Start()
    {
        doorBlock = transform.GetChild(0).gameObject;
        closedPosition = (Vector2)doorBlock.transform.position + Vector2.up;
        openPosition = closedPosition - Vector2.up;
        targetPos = closedPosition;
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = doorBlock.transform.position;
        
        if(currentPos == openPosition) {
            targetPos = closedPosition;
        }
        else if (currentPos == closedPosition) {
            targetPos = openPosition;
        }
        
        Vector2 targetDirection = (currentPos - targetPos).normalized;
        doorBlock.GetComponent<Rigidbody2D>().MovePosition(currentPos + targetDirection * Time.deltaTime);
    }
}
