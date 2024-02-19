using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boss_walk : MonoBehaviour, healthbarcontroller
{
    private int rand;
    public float enemyHealth = 7500, enemyMaxHealth = 7500, speed = 2.5f, attackRange = 2.0f, damagereduction = 0.1f, movspeed = 6, enemyatk = 50, atkspeed = 5; 
    Transform player;
    Rigidbody rb;
    Slider healthbar;
    public Animator anim;
    public UnityEngine.AI.NavMeshAgent nav;
    const string backslash = "Boss|BackSlash", breathattack = "Boss|BreathAttack", JumpSmash = "Boss|JumpSmash", slideslash = "Boss|SideSlash", bosssmash = "Boss|Smash";
    float sparkstatusdamage, orienemyatk = 60, markeddamage = 0;
    bool ded = false, ismarked = false;
    public GameObject showdamage, breathatk, slamatk, slamatk1;
    public normaleneysworddetection swordet;
    public bool burning = false, stillburning = false, sparked = false, chaosed = false;
    bool atking = false;
    float grenadeskill1stack = 0;
    public AudioSource[] audi;
    public Collider bossSword;
    menuscript menucon;
    void Start(){
        healthbar = GameObject.Find("BossEnemyHealthBar").GetComponent<Slider>();
        healthbar.maxValue = enemyHealth;
        healthbar.value = enemyHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        menucon = GameObject.Find("playerUI").GetComponent<menuscript>();
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(attackcycle());
    }
    void FixedUpdate(){    
        if(nav.isOnNavMesh == true&&atking==false){
            GoToNextPoint();
        }
    }
    public void GoToNextPoint(){
        if(player!=null){
            nav.destination = player.transform.position;  
        }
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
            da = (damage + (statusdamage * 0.1f)) - (damage + (statusdamage * 0.1f) * damagereduction) + ((damage + (statusdamage * 0.1f)) * grenadeskill1stack);
            enemyHealth -= da;
            da1 = (int)da;
            a = Instantiate(showdamage, transform.position, transform.rotation);
            a.GetComponent<TMP_Text>().text = da1.ToString();
        }else{
            da = damage - (damage * damagereduction) + (damage * grenadeskill1stack);
            enemyHealth -= da;
            da1 = (int)da;
            a = Instantiate(showdamage, transform.position, transform.rotation);
            a.GetComponent<TMP_Text>().text = da1.ToString();
        }
        sparkstatusdamage = statusdamage;
        if(sparked==true){
            enemyHealth -= (statusdamage * 3) - (statusdamage * 3 * damagereduction) + (statusdamage * grenadeskill1stack);
            healthbar.value = enemyHealth;
            a = Instantiate(showdamage, transform.position, transform.rotation);
            float b = statusdamage * 3;
            da1 = (int)b;
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
                swordet.damage = enemyatk;
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
        audi[0].Play();
        for(int i= 0; i<10;i++){
            yield return new WaitForSeconds(0.4f);
            float burndamage = enemyMaxHealth * 0.005f + ((enemyHealth * 0.015f) * (statusdamage * 0.1f));
            enemyHealth -= burndamage - (burndamage * damagereduction);
            healthbar.value = enemyHealth;
            int b = (int)burndamage;
            GameObject a = Instantiate(showdamage, transform.position, transform.rotation);
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
        swordet.damage = enemyatk;
    }
    IEnumerator attackcycle(){ 
        while(ded==false){
            yield return new WaitForSeconds(2.5f);
            rand = Random.Range(0, 5);
            switch(rand){
                case 0:
                    atking=true;
                    anim.Play(slideslash);
                    bossSword.enabled = true;
                    yield return new WaitForSeconds(1.5f);
                    bossSword.enabled = false;
                    break;
                case 1:
                    atking=true;
                    anim.Play(backslash);
                    bossSword.enabled = true;
                    yield return new WaitForSeconds(1.3f);
                    bossSword.enabled = false;
                    break;
                case 2:
                    anim.Play(JumpSmash);
                    nav.speed = movspeed*4;
                    yield return new WaitForSeconds(2);
                    Instantiate(slamatk, transform.position + new Vector3(0,1,0), transform.rotation);
                    nav.speed = movspeed;
                    break;
                case 3:
                    atking=true;
                    anim.Play(breathattack);
                    yield return new WaitForSeconds(1.25f);
                    Instantiate(breathatk, transform.position + new Vector3(0,3,0), transform.rotation);
                    break;
                case 4:
                    anim.Play(bosssmash);
                    nav.speed = movspeed*1.5f;
                    yield return new WaitForSeconds(1.25f);
                    Instantiate(slamatk1, transform.position, transform.rotation);
                    nav.speed = movspeed;
                    break;
            }
            atking=false;
        }
    }
    void die(){
        ded = true;
        menucon.win();
        audi[1].Stop();
        player.GetComponent<playermovement>().turnoffcon();
        Destroy(this.gameObject, 0);
    }
    public bool checkdead(){
        if(ded==true){
            return true;
        }
        return false;
    }
}
