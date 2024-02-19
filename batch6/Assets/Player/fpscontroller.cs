using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpscontroller : MonoBehaviour
{
    public float sensitivity = 0.5f;
    float horizontalMouse, verticalMouse, xRotation, yRotation;
    public Transform player;
    void FixedUpdate(){
        horizontalMouse = Input.GetAxis("Mouse X");
        verticalMouse = Input.GetAxis("Mouse Y");
        if(verticalMouse!=0){
            xRotation -= verticalMouse * sensitivity;
            xRotation = Mathf.Clamp(xRotation,-90,90);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
        if(horizontalMouse!=0){
            player.Rotate(Vector3.up*horizontalMouse*sensitivity);
        }
    }
}
