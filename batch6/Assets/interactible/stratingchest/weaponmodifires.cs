using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class weaponmodifires : MonoBehaviour
{
    public int howmanyroll=6, maxCommonBuff, maxRareBuff,maxLegendaryBuff, damagetype = 0;
    public int[] rolledbuffs;
    public GameObject canvas;
    public Image damagetypeshow;
    public Material[] materials;
    public TMP_Text txt;
    int[] rarity = {11,22,28};
    string[] rolledbuffstring = {"+0.4x Critical DMG","+10% Critical Chance","-10% Melee Swing Speed","-10% Charge Attack Charge Time","+30% Critical Chance but -20% Status Chance","+30% Status Chance but -20% Critical Chance","+1.5% Base Damage after each elimination. (Up to +15%, resets after 8s not killing an enemy)","-20% Melee Swing Speed after killing an Enemy with Basic Attacks for 2s","-20% Charge Time after killing an Enemy with M2 for 2s","+10% Status Chance","+15% Status DMG, -15% Weapon DMG","+15% Weapon DMG, -15% Status DMG","+20% Melee DMG, +20% Charge Attack Swing Speed","+20% Charge Attack DMG, +20% Charge Attack Charge Time", "+1.2x Critical DMG, -20% Swing Speed and Charge Time","+40% Total DMG against Bosses","+5% Status Effect Chance upon hitting an enemy. (Up to +50%, resets on a miss.)","+2.5% Critical Chance upon hitting an enemy. (Up to +25%, resets on a miss.)","When triggering Status Effect, spread it to all enemies within 8m","+0.4x Critical Damage for 4s when making 4 Crit in 2s (bonus can stack)","0.2x CritX on each Crit Hit. (Up to +2.0x and will be reset on a normal hit/miss or 6s not doing Critical Damage)","-10% Swing Speed and Charge Time for 2s on a Critical Hit (Up to -100%)","+100% base Weapon DMG on next hit after making Crit Hit 4 times in a row","+200% Melee damage, however Charge Attacks deals 0 damage","+200% Charge Attack damage, however Basic Attacks deals 0 damage","+65% Total Weapon DMG against enemies in Status Effect", "+15% Total Weapon DMG (Up to 150%) for 6s whenever triggering an elemental effect.","After killing an enemy, +30% movement speed for 5s and +50% Critical Chance for the next hit.","+5% Status DMG each time triggering Status Effect. (Up to 500%, resets after 8s not triggering any Status Effect)"};
    public string alltext;
    public void rollbuff(){
        damagetype = Random.Range(0,4);
        damagetypeshow.material = materials[damagetype];
        switch(damagetype){
            case 0:
                alltext += "True Raw (Physical) type<br>";
                break;
            case 1:
                alltext += "<color=#BF9600>Arclight / Flame type</color><br>";
                break;
            case 2:
                alltext += "<color=#0596FF>Spark / Electric type</color><br>";
                break;
            case 3:
                alltext += "<color=#9600FF>Chaos / Void type</color><br>";
                break;
        }
        for(int i=0;i<2;i++){
            int a = -1;
            while(a==-1){
                a = check(Random.Range(0,12));
            }
            rolledbuffs[i] = a;
            alltext += "<color=#47FF4B>";
            alltext += rolledbuffstring[a];
            alltext += "</color><br>";
        }
        for(int i=2;i<4;i++){
            int a = -1;
            while(a==-1){
                a = check(Random.Range(12,23));
            }
            rolledbuffs[i] = a;
            alltext += "<color=#61B4FF>";
            alltext += rolledbuffstring[a];
            alltext += "</color><br>";
        }
        for(int i=4;i<5;i++){
            int a = -1;
            while(a==-1){
                a = check(Random.Range(23,29));
            }
            rolledbuffs[i] = a;
            alltext += "<color=#D4AF37>";
            alltext += rolledbuffstring[a];
            alltext += "</color><br>";
        }
        txt.text = alltext;
    }
    int check(int a){
        bool b = false;
        for(int j=0;j<rolledbuffs.Length;j++){
            if(a==rolledbuffs[j]){
                b = true;
            }
        }
        if(b==false){
            return a;
        }else{
            return -1;
        }
    }
    public void showbuffs(){
        canvas.SetActive(true);
    }
    public void hidebuffs(){
        canvas.SetActive(false);
    }
}
