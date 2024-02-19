using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunscript : MonoBehaviour
{
    public GameObject bullet, guns, stillcooldownobject;
    public Transform gunpoint;
    public bool canattack = true, holdingdown = false, buttonup = false;
    public float basedamage = 5, guncooldown = 1, weapondamage, statusdamage, statuschange, critchange = 50, critdamage = 2, critteddamage;
    public AudioSource[] audi;
    public Slider dodgeslider;
    public Animator anim;
    bool checkup = false;
    Vector3 c = new Vector3(-0.763f,-0.5f,0.866f);
    void Start(){
        updateSkill();
    }
    public void updateSkill(){
        weapondamage = basedamage;
        //statusdamage = weapondamage;
        critteddamage = weapondamage * critdamage;
        bullet.GetComponent<projectileInterface>().updatestats(basedamage, statuschange, critchange, critteddamage);  
    }
    void Update(){
        if(Input.GetButton("Fire2")){
            guns.transform.localPosition = c;
            checkup = true;
        }else if(checkup == true){
            guns.transform.localPosition = new Vector3(0,-5,0);
            checkup = false;
        }
        
        if(Input.GetButtonDown("Fire1") && canattack == true && checkup == true){
            StartCoroutine(attackCooldown());
        }
    }

    IEnumerator attackCooldown(){
        canattack = false;
        if(stillcooldownobject!=null){
            stillcooldownobject.SetActive(false);
        }
        audi[0].Play();
        anim.SetTrigger("shoot");
        Instantiate(bullet, gunpoint.position, gunpoint.rotation);
        dodgeslider.value = 0;
        float a = 1 / guncooldown * 0.02f;
        float dodgeslidervalue = 0;
        while(dodgeslidervalue<1){
            dodgeslidervalue+=a;
            dodgeslider.value = dodgeslidervalue;
            yield return new WaitForSeconds(0.02f);
        }
        audi[1].Play();
        anim.SetTrigger("draw");
        if(stillcooldownobject!=null){
            stillcooldownobject.SetActive(true);
        }
        canattack = true;
    }
}
