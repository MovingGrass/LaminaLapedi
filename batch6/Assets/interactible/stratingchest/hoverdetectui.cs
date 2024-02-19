using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class hoverdetectui : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(transform.childCount > 0){
            transform.GetChild(0).GetComponent<weaponmodifires>().showbuffs();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(transform.childCount > 0){
            transform.GetChild(0).GetComponent<weaponmodifires>().hidebuffs();
        }
    }
}
