using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playermovement : MonoBehaviour, healthbarcontroller
{
    public float playerHealth = 100, playerMaxHealth = 100, movSpeed, jumpForce = 10, dashcooldown = 1, generacdr = 0, primaryskillcdr = 0, secondaryskillcdr = 0, damagereduction = 0.2f;
    public int exp = 0, level = 0, XP = 0, dashcount = 1;
    float movX = 0, movY = 0;
    public bool gounded = true, candash=true, inventoryopened = true, inRangeOfInteractible, lookingatportal = false;
    public GameObject[] weapons, weaponsShow;
    public GameObject inventory, interactUI, pickSkill, pausescreen, bossHealthbar;
    public Rigidbody rb;
    public Transform weaponpicked, buffcontainer;
    public IInteractible chest;
    public TMP_Text exptext,wepdesc;
    public Slider healthbar, expbar;
    public swordcontroller swordcon;
    public gunscript guncon;
    public fpscontroller fpscon;
    public menuscript menucon;
    public AudioSource[] audi;
    public int[] rolledbuffs;
    bool checkinteract = false, webbed = false, canmove = true, dashing = false, canesc = true;
    public GameObject canvas;
    float webspeed, yeetweeb;
    void Start(){
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(canvas);
        updateSkill();
    }
    public void updateSkill(){
        playerHealth = playerMaxHealth; //update health to maxhealth
        healthbar.maxValue = playerHealth;
        healthbar.value = playerHealth;
        webspeed = movSpeed/2;
    }
    public void status(int damagetype, float statusdamage){}
    public bool checkstatus(){return false;}
    public bool checkdead(){return false;}
    public void grenadekill1(float stack, float slowedby){}
    public void marked(float markdamage){}
    void Update(){
        if(Input.GetKeyDown("space") && gounded == true){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(testtt());
            gounded = false;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) &&  candash == true && dashing == false){
            if(dashcount>0){
                StartCoroutine(dashcool(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
            }
        }
        if(Input.GetKeyDown("i")){
            waitinventory();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && canesc == true){
            turnoffcon();
            pausescreen.SetActive(true);
        }
        if(Input.GetKeyDown("f") && inRangeOfInteractible == true){
            if(lookingatportal == true){
                level++;
                playerHealth = playerMaxHealth;
                healthbar.value = playerHealth;
                if(level < 4){
                    transform.rotation = Quaternion.Euler(0,180,0);
                    SceneManager.LoadScene("Area");
                }else{
                    transform.rotation = Quaternion.Euler(0,-90,0);
                    SceneManager.LoadScene ("endscene");
                    bossHealthbar.SetActive(true);
                }
                transform.position = new Vector3(0,1,0);
            }else if(chest is not null){    //PICK A CHEST
                chest.interacted();
                inRangeOfInteractible = false;
            }else{                       //PICK A WEAPON
                changeweapon(int.Parse(weaponpicked.gameObject.name), true);
            }
        }
        if(inRangeOfInteractible == true){
            interactUI.SetActive(true);
            checkinteract = true;
        }else if(checkinteract==true){
            interactUI.SetActive(false);
            checkinteract = false;
        }
    }
    IEnumerator testtt(){
        yield return new WaitForSeconds(1.5f);
        gounded = true;
    }   
    public void turnoffcon(){
        Cursor.lockState = CursorLockMode.None;
        canesc = false;
        canmove = false;
        fpscon.enabled = false;
        swordcon.enabled = false;
        guncon.enabled = false;
    }
    public void turnoncon(){
        Cursor.lockState = CursorLockMode.Locked;
        canesc = true;
        menucon.openMenu();
        canmove = true;
        fpscon.enabled = true;
        swordcon.enabled = true;
        guncon.enabled = true;
    }
    void FixedUpdate(){
        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");
        if(canmove==true){
            if((movX!=0||movY!=0)){
                if(audi[1].isPlaying==false){
                    audi[1].Play();
                }
                rb.AddRelativeForce(new Vector3(movX*movSpeed,0,movY*movSpeed),ForceMode.Impulse);
                rb.velocity= new Vector3(0,rb.velocity.y,0);
            }
        }
    }
    public void wepstats(int weaponused){
        foreach(GameObject a in weapons){
            a.SetActive(false);
        }
        weapons[weaponused].SetActive(true);
        swordcon.weaponused = weaponused;
        switch(weaponused){
            case 0://============================ SWORD ================================
                swordcon.basedamage = 300;
                swordcon.critchange = 15;
                swordcon.critdamage = 1.5f;
                swordcon.statuschange = 20;
                swordcon.atkspeed = 0.6f;
                swordcon.chargetime = 1.3f;
                break;
            case 1://============================ Axe ==================================
                swordcon.basedamage = 500;
                swordcon.critchange = 22;
                swordcon.critdamage = 1.8f;
                swordcon.statuschange = 40;
                swordcon.atkspeed = 1f;
                swordcon.chargetime = 2.1f;
                break;
        }
        swordcon.updateSkill();
        swordcon.giveBuffs(rolledbuffs.Length, rolledbuffs);
    }
    public void changeweapon(int weaponused, bool replace){
        if(weaponpicked!=null){
            swordcon.primaryskillup = true;
            audi[4].Play();
            weaponmodifires a = weaponpicked.GetComponent<weaponmodifires>();
            swordcon.damagetype = a.damagetype;
            rolledbuffs = a.rolledbuffs;
            wepdesc.text = a.alltext;
            weaponpicked.parent.parent.GetComponent<startingchestcontroller>().turnoff();
            Destroy(weaponpicked.gameObject, 0);        
        }
        wepstats(weaponused);
    }
    public void updateHealthBar(float damage, float statusdamage){
        audi[5].Play();
        playerHealth -= damage - (damage * damagereduction);
        healthbar.value = playerHealth;
        if(playerHealth<=0){
            die();
        }
    }
    public void ActivateSkillUI(){
        pickSkill.SetActive(true);
    }
    public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "ground"){
            gounded = true;
        }
    }
    void OnParticleCollision(GameObject other){
        exp++;
        if(exp>=50){
            XP++;
            audi[3].Play();
            exptext.text = XP.ToString();
            exp = 0;
        }
        expbar.value = exp;
    }
    IEnumerator dashcool(float movXraw, float movYraw){
        candash = false;
        dashcount--;
        dashing = true;
        audi[0].Play();
        canmove = false;
        rb.AddRelativeForce(movXraw * movSpeed * 1.75f, 0, movYraw * movSpeed * 1.75f,ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        dashing = false;
        canmove = true;
        yield return new WaitForSeconds(dashcooldown - 0.3f);
        dashcount++;
        candash = true;
    }
    void waitinventory(){
        if(inventoryopened == false){
            turnoffcon();
            inventory.SetActive(true);
            inventoryopened = true;
        }else{
            turnoncon();
            inventory.SetActive(false);
            inventoryopened = false;
        }
    }
    public void getwebbed(){
        audi[2].Play();
        webbed = true;
        movSpeed = webspeed;
        StartCoroutine(webdamage());
    }
    IEnumerator webdamage(){
        while(webbed == true){
            updateHealthBar(5,0);
            yield return new WaitForSeconds(1);
        }
    }
    public void stopwebbed(){
        webbed = false;
        movSpeed =  webspeed*2;
    }
    public void die(){
        turnoffcon();
        Destroy(canvas, 0);
        Destroy(this.gameObject, 0);
        SceneManager.LoadScene("SampleScene");
    }
}
