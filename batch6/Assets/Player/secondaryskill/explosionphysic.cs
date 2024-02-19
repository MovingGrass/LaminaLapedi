using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionphysic : MonoBehaviour
{
    playermovement playermov;
    public float damage = 1, damageonenemy = 0, slowenemy = 0, markdamage = 0, healthincrease = 0;
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "enemy"||collision.gameObject.tag == "boss"){
            healthbarcontroller a = collision.transform.GetComponent<healthbarcontroller>();
            if(a is not null){
                if(damageonenemy!=0){
                    a.grenadekill1(damageonenemy, slowenemy);
                }
                if(healthincrease!=0){
                    playermov = GameObject.Find("Player").GetComponent<playermovement>();
                    playermov.playerHealth =  healthincrease;
                    playermov.healthbar.value = playermov.playerHealth;
                    StartCoroutine(waitheal());
                }
                if(markdamage!=0){
                    a.marked(markdamage);
                }
                a.updateHealthBar(damage, 0);
            }
        }
    }
    IEnumerator waitheal(){
        yield return new WaitForSeconds(4);
        playermov.playerHealth -=  healthincrease;
        if(playermov.playerHealth<1){
            playermov.playerHealth = 1;
        }
        playermov.healthbar.value = playermov.playerHealth;
    }
}
