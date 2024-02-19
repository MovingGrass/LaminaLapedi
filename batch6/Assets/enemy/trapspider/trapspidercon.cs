using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class trapspidercon : MonoBehaviour, healthbarcontroller
{
    public float enemyHealth = 100, enemyMaxHealth = 100, enemyatk = 60, atkspeed = 1, movspeed = 5, damagereduction = 0.15f;
    public Slider healthbar;
    public Transform player, spawnpointpro;
    public UnityEngine.AI.NavMeshAgent nav;
    public GameObject atkeindicator, expdrop, showdamage;

    public GameObject sprkeddieeffect, pro;

    public bool burning = false, stillburning = false, sparked = false, chaosed = false;
    float grenadeskill1stack = 0;

    public bool inRange = false, canatk = true, inRangeMelee = false;

    public Animator anim;

    float sparkstatusdamage, orienemyatk = 60,  markeddamage = 0;

    public Collider col;
    public trapmeldet det;

    bool ded = false, ismarked = false;

    public AudioSource[] audi;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyHealth = enemyMaxHealth;
        healthbar.maxValue = enemyHealth;
        healthbar.value = enemyHealth;
        det.damage = enemyatk;
        pro.GetComponent<enemytrapprophysic>().damage = enemyatk;
    }
    void FixedUpdate()
    {    
        if(nav.isOnNavMesh == true && inRange == false){
            GoToNextPoint();
        }
        if(inRange == true && canatk == true){
            atk();
        }
        if(inRangeMelee == true && canatk == true){
            atkmel();
        }
    }

    public void GoToNextPoint(){
        nav.destination = player.transform.position;  
    }
    public void updateHealthBar(float damage, float statusdamage){
        if(healthbar.gameObject.activeSelf == false){
            healthbar.gameObject.SetActive(true);
        }
        float da;
        int da1;
        GameObject a;

        if(ismarked==true){
            da = markeddamage - (markeddamage * damagereduction) + (markeddamage * grenadeskill1stack);
            enemyHealth -= da;
            da1 = (int)da;
            a = Instantiate(showdamage, transform.position, transform.rotation);
            a.GetComponent<TMP_Text>().text = da1.ToString();
            ismarked = false;
        }

        if(burning == true){
            da = (damage + (statusdamage * 0.1f)) - (damage + (statusdamage * 0.1f) * damagereduction);
            enemyHealth -= da;
            da1 = (int)da;
            a = Instantiate(showdamage, transform.position, transform.rotation);
            a.GetComponent<TMP_Text>().text = da1.ToString();
        }else{
            da = damage - (damage * damagereduction);
            enemyHealth -= da;
            da1 = (int)da;
            a = Instantiate(showdamage, transform.position, transform.rotation);
            a.GetComponent<TMP_Text>().text = da1.ToString();
        }
        sparkstatusdamage = statusdamage;
        if(sparked==true){
            enemyHealth -= (statusdamage * 3) - (statusdamage * 3 * damagereduction);
            healthbar.value = enemyHealth;
            a = Instantiate(showdamage, transform.position, transform.rotation);
            float b = statusdamage * 3;
            da1 = (int)b;
            a = Instantiate(showdamage, transform.position, transform.rotation);
            a.GetComponent<TMP_Text>().text = da1.ToString();
        }

        healthbar.value = enemyHealth;
        if(enemyHealth <= 0 && ded == false){
            die();
        }
    }

    public void grenadekill1(float stack, float slowedby){
        grenadeskill1stack = stack;
        movspeed -= movspeed * slowedby;
    }
    public void marked(float markdamage){
        ismarked = true;
        markeddamage = markdamage;
    }

    Coroutine activechaosdur;
    public void status(int damagetype, float statusdamage){
        switch(damagetype){
            case 1:
                nav.destination = transform.position;
                burning = true;
                if(stillburning==false){
                    StartCoroutine(burndebuf(statusdamage));
                }
                break;
            case 2:
                if(sparked==false){
                    StartCoroutine(sparkdebuf());
                }
                break;
            case 3:
                enemyatk = orienemyatk;
                orienemyatk = enemyatk;
                enemyatk -= enemyatk * 0.2f;
                if(activechaosdur!=null){
                    StopCoroutine(activechaosdur);
                }
                activechaosdur = StartCoroutine(chaosdebuf());
                break;
        }
        if(enemyHealth <= 0 && ded == false){
            die();
        }
    }
    public bool checkstatus(){
        if(burning==true){
            return true;
        }
        if(sparked==true){
            return true;
        }
        if(chaosed==true){
            return true;
        }
        return false;
    }
    IEnumerator burndebuf(float statusdamage){
        stillburning = true;
        audi[1].Play();
        for(int i= 0; i<10;i++){
            yield return new WaitForSeconds(0.4f);
            float burndamage = enemyMaxHealth * 0.035f + ((enemyHealth * 0.15f) * (statusdamage * 0.1f));
            enemyHealth -= burndamage - (burndamage * damagereduction);
            healthbar.value = enemyHealth;
            GameObject a = Instantiate(showdamage, transform.position, transform.rotation);
            int b = (int)burndamage;
            a.GetComponent<TMP_Text>().text = b.ToString();
            Destroy(a,1);
            if(enemyHealth <= 0 && ded == false){
                die();
            }
        }
        stillburning = false;
        burning = false;
    }
    IEnumerator sparkdebuf(){
        sparked = true;
        float oriatkspeed = atkspeed;
        atkspeed -= atkspeed * 0.2f;
        float orimovspeed = movspeed;
        movspeed -= movspeed * 0.2f;
        nav.speed = movspeed;
        yield return new WaitForSeconds(6);
        atkspeed = oriatkspeed;
        movspeed = orimovspeed;
        nav.speed = movspeed;
        sparked = false;
    }
    IEnumerator chaosdebuf(){
        chaosed = true;
        yield return new WaitForSeconds(6);
        chaosed = false;
        enemyatk = orienemyatk;
    }

    public void atk(){
        StartCoroutine(atking());
        StartCoroutine(canatkwait());
    }

    public void atkmel(){
        StartCoroutine(atkingmel());
        StartCoroutine(canatkwait());
    }

    IEnumerator canatkwait(){
        canatk = false;
        yield return new WaitForSeconds(atkspeed);
        canatk = true;
    }

    IEnumerator atking(){
        GameObject a = Instantiate(atkeindicator,transform.position,transform.rotation);
        yield return new WaitForSeconds(0.2f);
        a.transform.parent = transform;
        a.transform.localPosition = new Vector3(0,2,0);
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("atkrange");
        yield return new WaitForSeconds(0.5f);
        audi[2].Play();
        Instantiate(pro,spawnpointpro.position,spawnpointpro.rotation);
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator atkingmel(){
        GameObject a = Instantiate(atkeindicator,transform.position,transform.rotation);
        yield return new WaitForSeconds(0.2f);
        a.transform.parent = transform;
        a.transform.localPosition = new Vector3(0,2,0);
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("atk");
        audi[0].Play();
        col.enabled = true;
        yield return new WaitForSeconds(0.5f);
        col.enabled = false;
    }

    public void die(){
        ded = true;
        StopAllCoroutines();
        this.enabled = false;
        nav.destination = transform.position;
        anim.SetTrigger("died");
        Instantiate(expdrop, transform.position, transform.rotation);
        if(sparked==true){
            GameObject a = Instantiate(sprkeddieeffect, transform.position - new Vector3(0,0,0), transform.rotation);
            a.GetComponent<sparkdieffectphysic>().statusdamage = sparkstatusdamage;
        }
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject, 0.75f);
    }
    public bool checkdead(){
        if(ded==true){
            return true;
        }
        return false;
    }
}
