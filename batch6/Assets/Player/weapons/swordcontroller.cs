using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class swordcontroller : MonoBehaviour
{
    public GameObject specialwepdetection, axespecialwepdetection, primaryskillpro,secondarypro , skillcontrol;
    public GameObject[] buffs, buffshit;    
    public bool primaryskillup = false, canattack = true, hold = false;
    public int combo = 0, damagetype = 0;
    public float basedamage = 10, weapondamage, statusdamage, statuschange, atkspeed = 0.6f, chargevalue = 0, chargetime = 1, critchange = 50, critdamage = 2, critteddamage, specialattackdamage = 30;
    public Animator swordanim;
    public ParticleSystem specialattackeffect;
    public gunscript guncon;
    public bool[] killEffectUnlocked, hiteffectunlocked, skillprimunlocked, skillsecondunlocked;
    public int weaponused = -1;
    public Transform player, buffcontainer, secondarypoint;
    private Vector3 lastFramePosition;
    public swordhitdetection detection, specialdetection, axedetection, axespecialdetection;
    public ParticleSystem[] particleBurst;
    public playermovement playermov;
    public Slider chargeslider;
    public bool inprimaryskill1 = false;
    public AudioSource[] audi;
    bool firstupdateskil = true;
    public TMP_Text statstxt;
    const string swatk = "swordanim";
    const string swatk1 = "New Animation2";
    const string axatk = "axeanim";
    const string axatk1 = "axeanim2";
    float primarydamage = 250, primaryduration = 1, traildamage = 25, secondarydamage = 225, secondaryslow = 0, seconhealthincrease=0, secondmarkdamage=0;
    bool secondaryskillup = true;
    public cooldowncontroller primcol,seconcol;
    public int[] skil1stack,skil2stack;
    Vector3 primsize = new Vector3(1,1,1);
    void Start(){
        foreach(ParticleSystem a in particleBurst){
            a.Stop();
        }
    }
    public void updateSkill(){
        statstxt.text = "";
        if(damagetype == 0){
            if(firstupdateskil==true){
                basedamage += basedamage * 0.3f;
                weapondamage = basedamage;
            }
            weapondamage = basedamage;
        }else if(damagetype!=0){
            weapondamage = basedamage * 0.85f;
        }
        updateTxt(weapondamage);
        statusdamage = basedamage * 0.15f;
        updateTxt(statusdamage);
        detection.damage = weapondamage; //update damage
        axedetection.damage = weapondamage;
        if(damagetype == 0&&firstupdateskil==true){
            detection.critchange = critchange + (critchange + 10);
            axedetection.critchange = critchange + (critchange + 10);
        }else{
            detection.critchange = critchange;
            axedetection.critchange = critchange;
        }
        updateTxt(critchange);
        if(damagetype == 0 && firstupdateskil==true){//INI BUAT CHECK CRIT DAMAGE
            float b = critdamage +  0.2f;
            detection.critteddamage = weapondamage * b;
            axedetection.critteddamage = weapondamage * b;
            updateTxt(b);
        }else{
            detection.critteddamage = weapondamage * critdamage;
            axedetection.critteddamage = weapondamage * critdamage;
            updateTxt(critdamage);
        }
        firstupdateskil=false;
        detection.statuschange = statuschange;
        axedetection.statuschange = statuschange;
        updateTxt(statuschange);
        detection.statusdamage = statusdamage;
        axedetection.statusdamage = statusdamage;
        updateTxt(statusdamage);
        updateTxt(atkspeed);
        updateTxt(chargetime);
        specialdetection.damage = weapondamage * 0.5f;
        axespecialdetection.damage = weapondamage * 0.5f;
        specialdetection.critchange = critchange;
        axespecialdetection.critchange = critchange;
        specialdetection.critteddamage = (weapondamage * 0.25f) * critdamage;
        axespecialdetection.critteddamage = (weapondamage * 0.25f) * critdamage;  
        specialdetection.statuschange = statuschange;
        axespecialdetection.statuschange = statuschange;
        specialdetection.statusdamage = (weapondamage * 0.25f) * 0.15f;
        axespecialdetection.statusdamage = (weapondamage * 0.25f) * 0.15f;
        detection.damagetype = damagetype;
        axedetection.damagetype = damagetype;
    }
    void updateTxt(float a){
        statstxt.text += a.ToString();
        statstxt.text += "<br>";
    }
    void FixedUpdate(){
        if(Input.GetButton("Fire1") && canattack == true){
            hold = true;
            StartCoroutine(Charging());
        }
        if(Input.GetButton("Fire1") == false && hold == true){
            hold = false;
        }
        if(Input.GetKey("q") && primaryskillup == true){
            StartCoroutine(primarycooldown());
        }
        if(Input.GetKey("e") && secondaryskillup == true){
            StartCoroutine(secondarycooldown());
        }
    }
    public void killedAnEnemyM1(){
        GameObject a;
        if(killEffectUnlocked[0]==true){
            a = GameObject.Find("killeffect0");
            if(a == null){
                a = Instantiate(buffs[0], buffcontainer);
                a.name = "killeffect0";
            }else{
                a.GetComponent<buffcooldowncontroller>().resetcooldown();
            }
        }
        if(killEffectUnlocked[1]==true){
            a = GameObject.Find("killeffect1");
            if(a == null){
                a = Instantiate(buffs[1], buffcontainer);
                a.name = "killeffect1";
            }else{
                a.GetComponent<buffcooldowncontroller>().resetcooldown();
            }
        }
        if(killEffectUnlocked[3]==true){
            a = GameObject.Find("killeffect3");
            if(a == null){
                a = Instantiate(buffs[3], buffcontainer);
                a.name = "killeffect3";
            }else{
                a.GetComponent<buffcooldowncontroller>().resetcooldown();
            }
        }
    }
    public void killedAnEnemyHoldM1(){
        GameObject a;
        if(killEffectUnlocked[2]==true){
            a = GameObject.Find("killeffect2");
            if(a == null){
                a = Instantiate(buffs[2], buffcontainer);
                a.name = "killeffect2";
            }else{
                a.GetComponent<buffcooldowncontroller>().resetcooldown();
            }
        }
    }
    public void BasicAttack(){
        combo++;    
        switch(weaponused){
            case 0:
                if(combo==1){
                    swordanim.Play(swatk);
                    audi[6].Play();
                }else{
                    swordanim.Play(swatk1);
                    audi[7].Play();
                    combo = 0;
                }
                if(detection.hittedenemy.Count == 0){
                    miss();
                }
                detection.hittedenemy.Clear();
                break;
            case 1:
                audi[3].Play();
                if(combo==1){
                    swordanim.Play(axatk);
                }else{
                    swordanim.Play(axatk1);
                    combo = 0;
                }
                if(axedetection.hittedenemy.Count == 0){
                    miss();
                }
                axedetection.hittedenemy.Clear();
                break;
        }
        if(inprimaryskill1 == true){
            GameObject a = Instantiate(primaryskillpro, transform.position, transform.rotation * Quaternion.Euler(0,0, 90 + combo * -90));
            primaryskillphysic b = a.GetComponent<primaryskillphysic>();
            if(skillprimunlocked[0]==true){
                b.damage = primarydamage;
            }
            if(skillprimunlocked[1]==true){
                b.duration = primaryduration;
            }
            if(skillprimunlocked[2]==true){
                a.transform.localScale = primsize;
            }
            if(skillprimunlocked[3]==true){
                a.transform.GetChild(0).gameObject.SetActive(true);
                a.transform.GetChild(0).GetComponent<traiphysic>().damage = traildamage;
            }
        }
    }
    public void upskill1(int num){
        audi[11].Play();
        switch(num){
            case 0:
                skil1stack[0]++;
                primarydamage = 250 * (skil1stack[0] + 1);
                break;
            case 1:
                skil1stack[1]++;
                primaryduration += 1;
                break;
            case 2:
                skil1stack[2]++;
                primsize += new Vector3(1,1,1);
                break;
            case 3:
                skil1stack[3]++;
                if(skil1stack[3]==3){
                    traildamage = 250 * (skil1stack[3] * 0.1f);
                }else{
                    traildamage = 250 * (skil1stack[3] * 0.025f);
                }
                break;
            case 4:
                skil1stack[4]++;
                break;
            case 5:
                skil1stack[5]++;
                break;
        }
        skillprimunlocked[num] = true;
        skillcontrol.SetActive(false);
    }
    public void upskill2(int num){
        audi[11].Play();
        switch(num){
            case 0:
                skil2stack[0]++;
                if(skil1stack[0]==3){
                    secondarydamage = 0.4f;
                    secondaryslow = 0.2f;
                }else{
                    secondarydamage = skil2stack[0] * 0.1f;
                    secondaryslow = skil2stack[0] * 0.05f;
                }
                break;
            case 1:
                skil2stack[1]++;
                secondarydamage += 225 * (skil2stack[1]);
                break;
            case 2:
                skil2stack[2]++;
                secondarydamage = 225 + (225 * (skil2stack[2] * 0.2f));
                break;
            case 3:
                break;
            case 4:
                skil2stack[3]++;
                seconhealthincrease = (skil2stack[3] * 5) + 5;
                break;
            case 5:
                skil2stack[4]++;
                if(skil2stack[4]==3){
                    secondmarkdamage = secondarydamage;
                }else{
                    secondmarkdamage = secondarydamage * (skil2stack[4] * 0.25f);
                }
                break;
        }
        skillsecondunlocked[num] = true;
        skillcontrol.SetActive(false);
    }
    IEnumerator primarycooldown(){
        StartCoroutine(dur());
        primcol.enabled = true;
        primaryskillup = false;
        yield return new WaitForSeconds(20);
        audi[10].Play();       
        primaryskillup = true;
    }
    IEnumerator secondarycooldown(){
        audi[13].Play();
        seconcol.enabled = false;
        seconcol.enabled = true;
        GameObject a = Instantiate(secondarypro,secondarypoint.position, secondarypoint.rotation);
        secondarykillphysic b = a.GetComponent<secondarykillphysic>();
        if(skillsecondunlocked[0]==true){
            b.damage = secondarydamage;
            b.slowedby = secondaryslow;
        }
        if(skillsecondunlocked[1]==true){
            b.damage = secondarydamage;
        }
        if(skillsecondunlocked[2]==true){
            float c = 0.5f * skil2stack[2];
            b.transform.localScale += new Vector3(c,c,c);
        }
        if(skillsecondunlocked[3]==true){
            b.skill3unlocked = true;
        }
        if(skillsecondunlocked[4]==true){
            b.healthincrease = seconhealthincrease;
        }
        if(skillsecondunlocked[5]==true){
            b.markdamage = secondmarkdamage;
        }
        secondaryskillup = false;
        yield return new WaitForSeconds(7);
        audi[13].Play();
        secondaryskillup = true;
    }
    IEnumerator dur(){
        inprimaryskill1 = true;
        float skill2primarydamagereduction,skill3primaryatkspeed,skill3primarymovSpeed, oridamagereduction = 0, oriatkspeed = 0, orimovespeed = 0;
        if(skillprimunlocked[3]==true){
            if(skil1stack[3]==3){
                traildamage = primarydamage * 0.1f;
            }else{
                traildamage = primarydamage * (0.025f * skil1stack[3]);
            }
        }
        if(skillprimunlocked[4]==true){
            oridamagereduction = playermov.damagereduction;
            skill2primarydamagereduction = oridamagereduction + (skil1stack[4] * 0.1f);
            playermov.damagereduction = skill2primarydamagereduction;
        }
        if(skillprimunlocked[5]==true){
            oriatkspeed = atkspeed;
            orimovespeed = playermov.movSpeed;
            if(skil1stack[5]==3){
                skill3primaryatkspeed = oriatkspeed - (oriatkspeed * 0.4f);
                skill3primarymovSpeed = orimovespeed + (orimovespeed * 0.1f);
            }else{
                skill3primaryatkspeed = oriatkspeed -(oriatkspeed * ((skil1stack[5] * 0.1f) + 0.1f));
                skill3primarymovSpeed = orimovespeed + (orimovespeed * 0.1f);
            }
            atkspeed = skill3primaryatkspeed;
            playermov.movSpeed = skill3primarymovSpeed;
        }
        yield return new WaitForSeconds(8);
        if(skillprimunlocked[4]==true){
            playermov.damagereduction = oridamagereduction;
        }
        if(skillprimunlocked[5]==true){
            atkspeed = oriatkspeed;
            playermov.movSpeed = orimovespeed;
        }
        inprimaryskill1 = false;
    }
    IEnumerator Charging(){
        BasicAttack();
        float a = 1 / chargetime * 0.02f;
        switch(weaponused){
            case 0:
                canattack = false;
                yield return new WaitForSeconds(atkspeed);
                audi[4].Play();
                while(hold == true){
                    chargevalue+=a;
                    chargeslider.value = chargevalue;
                    yield return new WaitForSeconds(0.02f);
                    if(chargevalue >= 1){
                        audi[8].Play();
                        StartCoroutine(specialatk());
                        break;
                    }
                }
                audi[4].Stop();
                break;
            case 1:
                canattack = false;
                yield return new WaitForSeconds(atkspeed);
                audi[5].Play();
                while(hold == true){
                    chargevalue+=a;
                    chargeslider.value = chargevalue;
                    yield return new WaitForSeconds(0.02f);
                    if(chargevalue >= 1){
                        audi[9].Play();
                        Instantiate(axespecialwepdetection, transform.position - new Vector3(0,1,2), Quaternion.Euler(0,0,0));
                        break;
                    }
                }
                audi[5].Stop();
                break;
        }
        chargevalue = 0;
        chargeslider.value = 0;
        canattack = true;
    }

    IEnumerator specialatk(){
        particleBurst[0].Play();
        for(int i = 0;i<11;i++){
            specialwepdetection.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            specialwepdetection.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        particleBurst[0].Stop();
    }

    public void hiteffect(){
        GameObject a;
        if(hiteffectunlocked[0]==true){
            a = GameObject.Find("hiteffect0");
            if(a == null){
                a = Instantiate(buffshit[0], buffcontainer);
                a.name = "hiteffect0";
            }else{
                a.GetComponent<hiteffectbuff>().increasestack();
            }
        }
        if(hiteffectunlocked[1]==true){
            a = GameObject.Find("hiteffect1");
            if(a == null){
                a = Instantiate(buffshit[1], buffcontainer);
                a.name = "hiteffect1";
            }else{
                a.GetComponent<hiteffectbuff>().increasestack();
            }
        }
    }
    public void miss(){
        GameObject a;
        if(hiteffectunlocked[0]==true){
            a = GameObject.Find("hiteffect0");
            if(a != null){
                a.GetComponent<hiteffectbuff>().resetOnMiss();
            }
        }
        if(hiteffectunlocked[1]==true){
            a = GameObject.Find("hiteffect1");
            if(a != null){
                a.GetComponent<hiteffectbuff>().resetOnMiss();
            }
        }
    }
    public void crithiteffect(){
        GameObject a;
        if(hiteffectunlocked[2]){
            a = GameObject.Find("hiteffect2");
            if(a == null){
                a = Instantiate(buffshit[2], buffcontainer);
                a.name = "hiteffect2";
            }else{
                a.GetComponent<buffcooldowncontroller>().resetcooldown();
            }
        }
        if(hiteffectunlocked[3]){
            a = GameObject.Find("hiteffect3");
            if(a == null){
                a = Instantiate(buffshit[3], buffcontainer);
                a.name = "hiteffect3";
            }else{
                a.GetComponent<buffcooldowncontroller>().resetcooldown();
            }
        }
        if(hiteffectunlocked[4]){
            a = GameObject.Find("hiteffect4");
            if(a == null){
                a = Instantiate(buffshit[4], buffcontainer);
                a.name = "hiteffect4";
            }
        }
        if(hiteffectunlocked[5]){
            a = GameObject.Find("hiteffect5");
            if(a == null){
                a = Instantiate(buffshit[5], buffcontainer);
                a.name = "hiteffect5";
            }else{
                a.GetComponent<hiteffectbuff>().increasestack();
            }
        }
    }
    public void statushit(){
        GameObject a;
        if(hiteffectunlocked[6]){
            a = GameObject.Find("hiteffect6");
            if(a == null){
                a = Instantiate(buffshit[6], buffcontainer);
                a.name = "hiteffect6";
            }else{
                a.GetComponent<hiteffectbuff>().increasestack();
            }
        }
        if(hiteffectunlocked[7]){
            a = GameObject.Find("hiteffect7");
            if(a == null){
                a = Instantiate(buffshit[7], buffcontainer);
                a.name = "hiteffect7";
            }else{
                a.GetComponent<buffcooldowncontroller>().resetcooldown();
            }
        }
    }
    public void giveBuffs(int size, int[] rolledbuffs){
        int killeffectsize = killEffectUnlocked.Length;
        for(int i = 0;i<killeffectsize;i++){
            killEffectUnlocked[i] = false;
        }
        int hiteffectsize = hiteffectunlocked.Length;
        for(int i = 0;i<hiteffectsize;i++){
            hiteffectunlocked[i] = false;
        }
        for(int i= 0;i<size;i++){
            theBuffs(rolledbuffs[i]);
        }
    }
    public void theBuffs(int pickedbuffs){
        switch(pickedbuffs){
            case 0:
                critdamage += 0.4f;
                updateSkill();
                break;
            case 1:
                critchange += critchange * 0.1f;
                updateSkill();
                break;
            case 2:
                atkspeed -= atkspeed * 0.1f;
                updateSkill();
                break;
            case 3:
                chargetime -= chargetime * 0.1f;
                updateSkill();
                break;
            case 4:
                critchange += critchange * 0.3f;
                statuschange -= statuschange * 0.2f;
                updateSkill();
                break;
            case 5:
                statuschange += statuschange * 0.3f;
                critchange -= critchange * 0.2f;
                updateSkill();
                break;
            case 6:
                killEffectUnlocked[0] = true;
                break;
            case 7:
                killEffectUnlocked[1] = true;
                break;
            case 8:
                killEffectUnlocked[2] = true;
                break;
            case 9:
                statuschange += statuschange * 0.1f;
                updateSkill();
                break;
            case 10:
                statusdamage += statusdamage * 0.15f;
                weapondamage -= weapondamage * 0.15f;
                updateSkill();
                break;
            case 11:
                weapondamage += weapondamage * 0.15f;
                statusdamage -= statusdamage * 0.15f;
                updateSkill();
                break;
            case 12:
                detection.damage += detection.damage * 0.2f;
                axedetection.damage += axedetection.damage * 0.2f;
                chargetime += chargetime * 0.2f;
                break;
            case 13:
                specialdetection.damage += specialdetection.damage * 0.2f;
                axespecialdetection.damage += axespecialdetection.damage * 0.2f;
                chargetime += chargetime * 0.2f;
                break;
            case 14:
                critdamage += 1.2f;
                atkspeed -= atkspeed * 0.2f;
                chargetime -= chargetime *0.2f;
                updateSkill();
                break;
            case 15:
                detection.killeffect3 = true;
                specialdetection.killeffect3 = true;
                axedetection.killeffect3 = true;
                axespecialdetection.killeffect3 = true;
                break;
            case 16:
                hiteffectunlocked[0] = true;
                break;
            case 17:
                hiteffectunlocked[1] = true;
                break;
            case 18:
                detection.killeffect4 = true;
                specialdetection.killeffect4 = true;
                axedetection.killeffect4 = true;
                axespecialdetection.killeffect4 = true;
                break;
            case 19:
                hiteffectunlocked[2] = true;
                break;
            case 20:
                hiteffectunlocked[3] = true;
                break;
            case 21:
                hiteffectunlocked[4] = true;
                break;
            case 22:
                hiteffectunlocked[5] = true;
                break;
            case 23:
                detection.damage *= 3;
                specialdetection.damage = 0;
                axedetection.damage *= 3;
                axespecialdetection.damage = 0;
                break;
            case 24:
                detection.damage = 0;
                specialdetection.damage *= 3;
                axedetection.damage = 0;
                axespecialdetection.damage *= 3;
                break;
            case 25:
                detection.killeffect5 = true;
                specialdetection.killeffect5 = true;
                axedetection.killeffect5 = true;
                axespecialdetection.killeffect5 = true;
                break;
            case 26:
                hiteffectunlocked[6]=true;
                break;
            case 27:
                killEffectUnlocked[3]=true;
                break;
            case 28:
                hiteffectunlocked[7]=true;
                break;
        }
    }
    public void sound(int damagetype){
        switch(damagetype){
            case 1:
                audi[0].Play();
                break;
            case 2:
                audi[1].Play();
                break;
            case 3:
                //audi[2].Play();
                break;
        }
    }
}
