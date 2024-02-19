using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytrapprophysic : MonoBehaviour
{
    public float damage;
    public Rigidbody rb;
    public GameObject ex;
    public bool isex = false, ispit = false;
    bool playerinweb = false;
    playermovement playermov;
    void Start(){
        if(isex==false){
            rb.AddRelativeForce(Vector3.forward * 15,ForceMode.Impulse);
        }   
        if(ispit == true){
            rb.AddRelativeForce(Vector3.forward * 10,ForceMode.Impulse);
            Invoke("die", 3);
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "ground"){
            if(isex==false){
                die();
            }
        }
    }
    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Player"){
            if(playermov==null){
                playermov = collision.gameObject.GetComponent<playermovement>();
            }
            if(ispit==true){
                playermov.updateHealthBar(damage,0);
                die();
                return;
            }
            if(isex==true){
                playermov.getwebbed();
                playerinweb = true;
            }
        }
    }

    void OnTriggerExit(Collider collision){
        if(collision.gameObject.tag == "Player"){
            if(isex==true){
                playermov.stopwebbed();
                playerinweb = false;
            }
        }
    }

    void OnDestroy(){
        if(playerinweb==true && isex==true){
            playermov.stopwebbed();
        }
    }

    void die(){
        rb.velocity = new Vector3(0,0,0);
        gameObject.GetComponent<Collider>().enabled = false;
        if(isex==false){
            Transform a = Instantiate(ex, transform.position, Quaternion.Euler(0,0,0)).transform;
            a.rotation = Quaternion.Euler(0,0,0);
            a.GetComponent<enemytrapprophysic>().damage = damage;
        }
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(this.gameObject,1);
    }
}
