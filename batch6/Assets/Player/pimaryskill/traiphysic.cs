using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traiphysic : MonoBehaviour
{
    public float damage = 1;
    void Start(){
        GetComponent<AudioSource>().Play();
        StartCoroutine(die());
    }
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "enemy"){
            Debug.Log("test");
            healthbarcontroller a = collision.transform.GetComponent<healthbarcontroller>();
            if(a is not null){
                a.updateHealthBar(damage, 0);
            }
        }
    }
    IEnumerator die(){
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject,0);
    }
}