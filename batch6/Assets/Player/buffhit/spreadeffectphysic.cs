using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spreadeffectphysic : MonoBehaviour
{
    public int damagetype = 0;
    public float statusdamage;
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Player"){
            healthbarcontroller a = collision.transform.GetComponent<healthbarcontroller>();
            if(a is not null){
                a.status(damagetype,statusdamage);
            }
        }
    }
}
