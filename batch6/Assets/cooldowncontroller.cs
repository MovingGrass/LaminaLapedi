using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cooldowncontroller : MonoBehaviour
{
    public float cool = 1;
    public Image col;
    public TMP_Text txt;
    public bool havetxt = true, reduce = true;

    float d=0,b=0,c=0;

    void OnEnable(){
        if(havetxt==true){
            txt.gameObject.SetActive(true);
        }
        c = 1 / cool * 0.02f;
        b = cool / 0.02f;
        b = cool / (b - 1);
        if(reduce==true){
            col.fillAmount = 1;
            d = cool;
        }else{
            col.fillAmount = 0;
            d = 0;
        }
    }
    void OnDisable()
    {
        d = 0;
        if(havetxt==true){
            txt.gameObject.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        if(reduce==true){
            col.fillAmount -= c;
            d -= b;
            if (d < 0){
                this.enabled = false;
            }
        }else{
            col.fillAmount += c;
            if (col.fillAmount >= 1){
                this.enabled = false;
            }
        }
        if(havetxt==true){
            txt.text = string.Format("{0:0.00}", d);
        }
    }
}
