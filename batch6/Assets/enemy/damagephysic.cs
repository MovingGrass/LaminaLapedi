using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagephysic : MonoBehaviour
{
    public Rigidbody rb;
    void OnEnable(){
        Destroy(this.gameObject, 1);
        rb.velocity = new Vector3(Random.Range(-10, 10), Random.Range(5, 6), Random.Range(-10, 10));
    }

}
