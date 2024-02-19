using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normaleneysworddetection : MonoBehaviour
{
    public float damage = 10;
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Player"){
            healthbarcontroller a = collision.transform.GetComponent<healthbarcontroller>();
            if(a is not null){
                a.updateHealthBar(damage - (damage * 0.2f), 0);
            }
        }
    }
}
