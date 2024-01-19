using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform NewGameMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        NewGameMenuButton.GetComponent<Animator>().Play("Hover Off");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        NewGameMenuButton.GetComponent<Animator>().Play("Hover On");
    
    }

     public void OnPointerExit(PointerEventData eventData)
    {
        NewGameMenuButton.GetComponent<Animator>().Play("Hover Off");
  
    }
}