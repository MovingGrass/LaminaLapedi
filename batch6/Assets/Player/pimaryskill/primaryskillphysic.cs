using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class primaryskillphysic : MonoBehaviour
{
    public float damage = 1, duration = 1;
    public GameObject traildamage;
    public bool skill1unlocked = false;
    void Start()
    {   if(skill1unlocked==true){
            traildamage.SetActive(true);
        }
        StartCoroutine(waittodie());
    }
    void FixedUpdate(){
        transform.Translate(Vector3.forward * 0.6f);
    }
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "enemy"||collision.gameObject.tag == "boss"){
            healthbarcontroller a = collision.transform.GetComponent<healthbarcontroller>();
            if(a is not null){
                a.updateHealthBar(damage, 0);
            }
        }
        if(collision.gameObject.tag == "wall"){
            die();
        }
    }
    IEnumerator waittodie(){
        yield return new WaitForSeconds(duration);
        die();
    }
    public void die(){
        GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject, 0.25f);
    }
}
