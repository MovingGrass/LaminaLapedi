using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buffcooldowncontroller : MonoBehaviour
{
    public int killeffect = 0, stacks = 0;
    public bool stacking = false;
    public float cool = 1;
    public Image col;
    public TMP_Text txt, stacktxt;
    float d=0,b=0,c=0;
    public swordcontroller swordcon;
    public playermovement playermov;
    public GameObject a;
    float oribasedamage, oriatkspeed, orichargetime, oricritdamage, oricritchange, orimovespeed;
    void OnEnable(){
        a = GameObject.Find("Head");

        swordcon = a.GetComponent<swordcontroller>();
        playermov = GameObject.FindWithTag("Player").GetComponent<playermovement>();
        oribasedamage = swordcon.basedamage;
        c = 1 / cool * 0.02f;
        b = cool / 0.02f;
        b = cool / (b - 1);

        col.fillAmount = 1;
        d = cool;

        if(stacking == false){        //Instantbuff
            switch(killeffect){
                case 0:
                    oribasedamage = swordcon.basedamage;
                    break;
                case 1:
                    oriatkspeed = swordcon.atkspeed;
                    swordcon.atkspeed -= oriatkspeed * 0.2f;
                    swordcon.updateSkill();
                    break;
                case 2:
                    orichargetime = swordcon.chargetime;
                    swordcon.chargetime -= orichargetime * 0.2f;
                    swordcon.updateSkill();
                    break;
                case 3:
                    oricritdamage = swordcon.critdamage;
                    break;
                case 4:
                    oricritdamage = swordcon.critdamage;
                    break;
                case 5:
                    oriatkspeed = swordcon.atkspeed;
                    orichargetime = swordcon.chargetime;
                    break;
                case 6:
                    oribasedamage = swordcon.basedamage;
                    break;
                case 7:
                    orimovespeed = playermov.movSpeed;
                    playermov.movSpeed += playermov.movSpeed * 0.3f;
                    oricritchange = swordcon.critchange;
                    playermov.updateSkill();
                    break;
            }
        }else{
            stacks++;
            stacktxt.text = stacks.ToString();
            skilleffect();
        }
    }

    void OnDestroy(){ //BUFFNYA HILANG
        if(oribasedamage != 0){
            swordcon.basedamage = oribasedamage;
        }
        if(oriatkspeed != 0){
            swordcon.atkspeed = oriatkspeed;
        }
        if(orichargetime != 0){
            swordcon.chargetime = orichargetime;
        }
        if(oricritdamage != 0){
            swordcon.critdamage = oricritdamage;
        }
        if(oricritchange!=0){
            swordcon.critchange = oricritchange;
        }
        if(orimovespeed!=0){
            playermov.movSpeed = orimovespeed;
        }
        swordcon.updateSkill();
    }
    void FixedUpdate()
    {
        col.fillAmount -= c;
        d -= b;
        if (d <= 0.01)
        {
            Destroy(this.gameObject,0);
        }
        txt.text = string.Format("{0:0.00}", d);
    }

    public void resetcooldown(){
        col.fillAmount = 1;
        d = cool;

        if(stacking == true){
            stacks++;
            stacktxt.text = stacks.ToString();
        }
        skilleffect();
    }

    void skilleffect(){
        switch(killeffect){
            case 0:
                if(stacks>10){
                    stacks = 10;
                }else{
                    stacktxt.text = stacks.ToString();
                    swordcon.basedamage = oribasedamage;
                    Debug.Log(oribasedamage);
                    swordcon.basedamage += (0.015f * stacks) * swordcon.basedamage;
                    swordcon.updateSkill();
                }
                break;
            case 3:
                if(stacks>10){
                    stacks = 10;
                }else{
                    stacktxt.text = stacks.ToString();
                    swordcon.critdamage += 0.4f;
                    swordcon.updateSkill();
                }
                break;
            case 4:
                if(stacks>10){
                    stacks = 10;
                }else{
                    stacktxt.text = stacks.ToString();
                    swordcon.critdamage += 0.2f;
                    swordcon.updateSkill();
                }
                break;
            case 5:
                if(stacks>10){
                    stacks = 10;
                }else{
                    stacktxt.text = stacks.ToString();
                    swordcon.atkspeed -= oriatkspeed * 0.1f;
                    swordcon.chargetime -= orichargetime * 0.1f;
                    swordcon.updateSkill();
                }
                break;
            case 6:
                if(stacks>10){
                    stacks = 10;
                }else{
                    stacktxt.text = stacks.ToString();
                    swordcon.basedamage += oribasedamage * 0.15f;
                    swordcon.updateSkill();
                }
                break;
            case 7:
                if(stacks>1){
                    stacks = 0;
                }else{
                    swordcon.critchange += 50;
                    swordcon.updateSkill();
                }
                break;
            case 8:
                if(stacks>100){
                    stacks = 100;
                }else{
                    stacktxt.text = stacks.ToString();
                    swordcon.statusdamage += 0.05f;
                }
                break;
        }
    }
}
