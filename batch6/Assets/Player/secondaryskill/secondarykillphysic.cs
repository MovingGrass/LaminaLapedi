using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondarykillphysic : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public GameObject effect;

    public bool skill3unlocked = false;
    public float damage = 225, slowedby = 0, healthincrease = 0, markdamage = 0;

    public AudioSource audi;

    void Start()
    {
        StartCoroutine(waittodie());
        rb.AddRelativeForce(Vector3.forward * 2, ForceMode.Impulse);
    }

    IEnumerator waittodie(){
        yield return new WaitForSeconds(1);
        StartCoroutine(die());
    }
    public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "enemy"){
            StartCoroutine(die());
        }
        if(collision.gameObject.tag == "boss"){
            StartCoroutine(die());
        }
    }
    IEnumerator die(){
        audi.Play();
        gameObject.GetComponent<Collider>().enabled = false;
        if(skill3unlocked==true){
            Instantiate(effect,transform.position, Quaternion.Euler(0,0,0));
            yield return new WaitForSeconds(0.4f);
        }
        GameObject a = Instantiate(effect,transform.position, Quaternion.Euler(0,0,0));
        a.transform.localScale = transform.localScale;
        explosionphysic b = a.GetComponent<explosionphysic>();
        b.damage = damage;
        if(slowedby!=0){
            b.slowenemy = slowedby;
        }
        if(healthincrease!=0){
            b.healthincrease = healthincrease;
        }
        if(markdamage!=0){
            b.markdamage = this.markdamage;
        }
        GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject, 0.25f);
    }
}
