using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytraprangemelee : MonoBehaviour
{
    public trapspidercon enemy;
    public bool canatk = true;
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Player"){
            if(canatk == true){
                enemy.inRangeMelee = true;
            }
        }
    }
    public void OnTriggerExit(Collider collision){
        if(collision.gameObject.tag == "Player"){
            if(canatk == true){
                enemy.inRangeMelee = false;
            }
        }
    }
}