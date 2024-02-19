using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillcontroller : MonoBehaviour
{
    public GameObject[] skills, skillshow;
    public playermovement playermov;
    void OnEnable(){
        playermov.turnoffcon();
        for(int i = 0;i<3;i++){
            Instantiate(skills[Random.Range(0,skills.Length)],transform);
        }
    }
    void OnDisable(){
        playermov.turnoncon();
        while (transform.childCount > 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
