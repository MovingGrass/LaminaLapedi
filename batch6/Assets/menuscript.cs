using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class menuscript : MonoBehaviour
{
    public AudioMixer audimix;
    public Slider bgmslide, sfxslide, masterslide, sensslide;
    public GameObject winUI;
    public GameObject[] activeFirst;
    public fpscontroller fpscon;
    bool inpano=true, rot=true;
    float angle = 0;
    public Transform head;
    void Start(){
        StartCoroutine(panorama());
    }
    public void cursorlockState(){
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void cursorlockStateNone(){
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void exitgame(){
        Application.Quit();
    }
    public void openMenu(){
        audimix.SetFloat("BGM", -20);
    }
    public void changeMastervol(){
        audimix.SetFloat("Master", masterslide.value);
    }
    public void changeBgmvol(){
        audimix.SetFloat("BGM", bgmslide.value);
    }
    public void changeSfxvol(){
        audimix.SetFloat("SFX", sfxslide.value);
    }
    public void changesens(){
        fpscon.sensitivity = sensslide.value;
    }
    public void onMenu(){
        foreach(GameObject a in activeFirst){
            a.SetActive(false);
        }
        inpano=true;
        StartCoroutine(panorama());
    }
    public void offMenu(){
        foreach(GameObject a in activeFirst){
            a.SetActive(true);
        }
        inpano=false;
    }
    IEnumerator panorama(){
        float b=0.25f;
        while(inpano==true){
            b-=0.001f;
            if(rot==true){
                angle+=b;
            }else{
                angle-=b;
            }
            if(angle>10){
                b=0.225f;
                rot=false;
            }else if(angle<-10){
                b=0.225f;
                rot=true;
            }
            head.localRotation = Quaternion.Euler(0,angle,0);
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void win(){
        winUI.SetActive(true);
    }
}
