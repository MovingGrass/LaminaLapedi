using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hiteffectbuff : MonoBehaviour
{
    public int hitbuffeffect = -1;
    swordcontroller swordcon;
    float oribasedamage, oriatkspeed, orichargetime, oristatuschange, oricritchange, oricritdamage;
    public TMP_Text txt;
    int stack = 0;
    void OnEnable(){
        swordcon = GameObject.Find("Head").GetComponent<swordcontroller>();
        oristatuschange = swordcon.statuschange;
        oricritchange = swordcon.critchange;
        oribasedamage = swordcon.basedamage;
    }
    void OnDestroy(){
        if(oristatuschange!=0){
            swordcon.statuschange = oristatuschange;
        }
        if(oricritchange!=0){
            swordcon.critchange = oricritchange;
        }
        if(oribasedamage!=0){
            swordcon.basedamage = oribasedamage;
        }
    }
    public void increasestack(){
        stack++;
        bufef();
    }
    public void resetOnMiss(){
        Destroy(this.gameObject,0);
    }
    public void bufef(){
        switch(hitbuffeffect){
            case 0:
                if(stack < 10){
                    swordcon.statuschange += 5;
                    txt.text = stack.ToString();
                }else{
                    stack = 10;
                }
                break;
            case 1:
                if(stack < 10){
                    swordcon.critchange += 2.5f;
                    txt.text = stack.ToString();
                }else{
                    stack = 10;
                }
                swordcon.updateSkill();
                break;
            case 5:
                if(stack >= 4){
                    swordcon.basedamage *= 2;
                    stack = 0;
                }else{
                    txt.text = stack.ToString();
                }
                swordcon.updateSkill();
                break;
            case 7:
                break;
        }
    }
}
