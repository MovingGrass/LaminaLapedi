using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestcontroller : MonoBehaviour, IInteractible
{

    playermovement playermov;
    public bool opened = false;
    public BoxCollider a;
    public GameObject child;

    public Animator anim;

    public void Start(){
        playermov = GameObject.Find("Player").GetComponent<playermovement>();
    }

    public void interacted(){
        if(opened == false){
            if(child!=null){
                child.SetActive(true);
            }
            anim.SetTrigger("openchest");
            playermov.ActivateSkillUI();
            a.enabled = false;
            opened = true;
        }
    }
}
