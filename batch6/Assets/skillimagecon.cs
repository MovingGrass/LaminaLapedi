using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillimagecon : MonoBehaviour
{
    public bool primary = true;
    GameObject[] skillshow;
    swordcontroller swordcon;
    void Start(){
        skillshow = transform.parent.GetComponent<skillcontroller>().skillshow;
        swordcon = GameObject.Find("Head").GetComponent<swordcontroller>();
    }

    public void click(int num){
        if(primary==true){
            skillshow[num].SetActive(false);
            swordcon.upskill1(num);
        }else{
            skillshow[num+3].SetActive(false);
            swordcon.upskill2(num);
        }
    }
}
