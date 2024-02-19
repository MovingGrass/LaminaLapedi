using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class uiskillhoverdetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform desc;
    public string textt;
    public TMP_Text txt;
    public void OnPointerEnter(PointerEventData eventData){
        desc.SetParent(transform, false);
        desc.localPosition = new Vector3(0,75,0);
        txt.text = textt;
        desc.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData){
        desc.gameObject.SetActive(false);
    }
}
