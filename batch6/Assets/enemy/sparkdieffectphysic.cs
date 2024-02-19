using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkdieffectphysic : MonoBehaviour
{
    public float statusdamage;
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Player"){
            healthbarcontroller a = collision.transform.GetComponent<healthbarcontroller>();
            if(a is not null){
                a.status(2,statusdamage);
            }
        }
    }
}
