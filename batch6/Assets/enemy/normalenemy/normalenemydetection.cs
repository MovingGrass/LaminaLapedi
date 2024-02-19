using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalenemydetection : MonoBehaviour
{
    public normalenemyphysic enemy;
    public bool canatk = true;
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Player"){
            if(canatk == true){
                enemy.atk();
            }
        }
    }
}
