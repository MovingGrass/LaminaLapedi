using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingchestcontroller : MonoBehaviour, IInteractible
{
    public GameObject option;
    public GameObject[] weapons;
    bool opened = false;
    public BoxCollider colider;
    public Animator anim;
    public void interacted(){
        if(opened == false){
            anim.SetTrigger("openchest");
            for(int i = 0; i<3; i++){
                int num = Random.Range(0,weapons.Length);
                GameObject a = Instantiate(weapons[num], transform);
                a.GetComponent<weaponmodifires>().rollbuff();
                a.transform.parent = option.transform;
                a.transform.localPosition = new Vector3(i * 3 - 3, 2, 0);
                a.transform.localRotation = Quaternion.Euler(0,-90,0);
                a.name = num.ToString();
            }
            option.SetActive(true);
            colider.enabled = false;
            opened = true;
            this.enabled = true;
        }
    }
    public void turnoff(){
        option.SetActive(false);
        this.enabled = false;
    }
}