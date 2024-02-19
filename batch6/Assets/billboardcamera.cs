using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboardcamera : MonoBehaviour
{
    public Transform cam;

    void Start(){
        cam = GameObject.Find("Main Camera").transform;
    }

    void LateUpdate()
    {
        if(cam!=null){
            transform.LookAt(transform.position + cam.forward * -1);
        }
    }
}
