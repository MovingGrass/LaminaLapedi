using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletphysic : MonoBehaviour, projectileInterface
{
    public float damage = 1, statuschange, critchange, critteddamage;
    Transform hittedthing;
    public Rigidbody rb;
    public void updatestats(float d, float sc, float cc, float cd){
        damage = d;
        statuschange = sc;
        critchange = cc;
        critteddamage = cd;
    }
    public bool critcheck(){
        if(Random.Range(0,101) <= critchange){
            return true;
        }
        return false;
    }
    public bool statuscheck(){
        if(Random.Range(0,101) <= statuschange){
            return true;
        }
        return false;
    }
    void Start(){
        rb.AddRelativeForce(Vector3.forward * 40, ForceMode.Impulse);
        StartCoroutine(waittodie());
    }
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "enemy"||collision.gameObject.tag == "boss"){
            hittedthing = collision.transform;
            healthbarcontroller a = collision.transform.GetComponent<healthbarcontroller>();
            if(a is not null){
                if(critcheck()==true){
                    a.updateHealthBar(critteddamage, 0);
                }else{
                    a.updateHealthBar(damage, 0);
                }

                if(statuscheck()==true){
                    Debug.Log("KENA STATUS");
                }
            }
            die();
        }
        if(collision.gameObject.tag == "ground" || collision.gameObject.tag == "wall"){
            hittedthing = collision.transform;
            die();
        }
    }

    IEnumerator waittodie(){
        yield return new WaitForSeconds(2);
        die();
    }

    public void die(){
        GetComponent<Collider>().enabled = false;
        rb.velocity = new Vector3(0,0,0);
        if(hittedthing!=null){
            transform.parent = hittedthing;
        }
        rb.velocity = new Vector3(0,0,0);
        Destroy(this.gameObject, 1);
    }
}
