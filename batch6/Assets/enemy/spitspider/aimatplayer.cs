using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimatplayer : MonoBehaviour
{
    public Transform player;

    void Start(){
        player = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + player.forward * -1);
    }
}
