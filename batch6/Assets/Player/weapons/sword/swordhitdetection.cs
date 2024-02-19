using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordhitdetection : MonoBehaviour
{
    public float critchange, statuschange, damage = 1, critteddamage, statusdamage;
    public int damagetype = 0;
    public swordcontroller swordcon;
    public List<GameObject> hittedenemy = new List<GameObject>();
    public int chaosstack = 0;
    public bool killeffect3 = false, special = false, killeffect4 = false, killeffect5 = false;
    public GameObject spreadeffect;
    void Start(){
        if(swordcon==null){
            swordcon=GameObject.Find("Head").GetComponent<swordcontroller>();
        }
    }

    public bool critcheck(){
        if(Random.Range(0,101) <= critchange){
            return true;
        }
        return false;
    }
    public bool statuscheck(){
        if(Random.Range(0,101) <= statuschange){
            return true;
        }
        return false;
    }
    Coroutine activechaosdur;
    float oricritteddamage, oridamage, oristatusdamage;
    public void OnTriggerEnter(Collider collision){
        healthbarcontroller enemycon = null;
        if(collision.gameObject.tag == "enemy"){
            if(hittedenemy.Contains(collision.gameObject)==false){
                //hittedenemy.Add(collision.gameObject);         //mungkin masalah?
                swordcon.hiteffect();
                enemycon = collision.transform.GetComponent<healthbarcontroller>();
                oricritteddamage = critteddamage; oridamage = damage; oristatusdamage = statusdamage;
                if(killeffect5==true && enemycon.checkstatus() == true){
                    critteddamage += critteddamage * 0.65f;
                    damage += damage * 0.65f;
                    statusdamage += statusdamage * 0.65f;
                }
                calculatedmg(enemycon, oricritteddamage, oridamage, oristatusdamage, collision.transform);
            }
        }
        if(collision.gameObject.tag == "boss"){
            if(hittedenemy.Contains(collision.gameObject)==false){
                hittedenemy.Add(collision.gameObject);         //mungkin masalah?
                swordcon.hiteffect();
                enemycon = collision.transform.GetComponent<healthbarcontroller>();
                oricritteddamage = critteddamage; oridamage = damage; oristatusdamage = statusdamage;
                if(killeffect5==true && enemycon.checkstatus() == true){
                    critteddamage += critteddamage * 0.65f;
                    damage += damage * 0.65f;
                    statusdamage += statusdamage * 0.65f;
                }
                if(killeffect3 == true){
                    critteddamage += critteddamage * 0.4f;
                    damage += damage * 0.4f;
                    statusdamage += statusdamage * 0.4f;
                }
                calculatedmg(enemycon, oricritteddamage, oridamage, oristatusdamage, collision.transform);
            }
        }
        if(enemycon!=null){
            if(special==false){
                StartCoroutine(test(enemycon));
            }else{
                StartCoroutine(test1(enemycon));
            }
        }
    }
    void calculatedmg(healthbarcontroller a, float oricritdamage, float oridamag, float oristusdamage, Transform collision){
        if(a is not null){
            if(critcheck()==true){
                swordcon.crithiteffect();
                a.updateHealthBar(critteddamage, statusdamage);
            }else{
                a.updateHealthBar(damage, statusdamage);
            }
            if(statuscheck()==true){
                a.updateHealthBar(statusdamage, statusdamage);
                a.status(damagetype, statusdamage);
                swordcon.sound(damagetype);
                swordcon.statushit();
                if(damagetype==3){
                    if(activechaosdur!=null){
                        StopCoroutine(activechaosdur);
                    }
                    activechaosdur = StartCoroutine(chaosdebufdur());
                    chaosdebuf();
                }
                if(killeffect4 == true){
                    GameObject b = Instantiate(spreadeffect, collision.transform);
                    spreadeffectphysic c = b.GetComponent<spreadeffectphysic>();
                    c.damagetype = damagetype;
                    c.statusdamage = statusdamage;
                }
            }
        }
        critteddamage = oricritdamage;
        damage = oridamag;
        statusdamage = oristusdamage;
    }
    IEnumerator test(healthbarcontroller a){
        yield return new WaitForSeconds(0.2f);
        if(a.checkdead() == true){
            swordcon.killedAnEnemyM1();
        }
    }
    IEnumerator test1(healthbarcontroller a){
        yield return new WaitForSeconds(0.2f);
        if(a.checkdead() == true){
            swordcon.killedAnEnemyHoldM1();
        }
    }
    IEnumerator chaosdebufdur(){
        yield return new WaitForSeconds(6);
        damage = oridamage;
        chaosstack = 0;
    }
    void chaosdebuf(){
        chaosstack++;
        damage += oridamage * 0.025f;
    }
}
