using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestraycast : MonoBehaviour
{
    public GameObject Player;
    public playermovement playermov;
    int layerMask;
    RaycastHit hit;
    private bool raycastelseenabler = false;
    void Start(){
        layerMask = 1 << 9;
        StartCoroutine(a());
    }
    IEnumerator a(){
        yield return new WaitForSeconds(0.2f);
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4, layerMask)){//hit
            Debug.Log("hit!!");
            switch(hit.transform.gameObject.tag){
                case "pickweapon":
                    playermov.weaponpicked = hit.transform;
                    hit.transform.gameObject.GetComponent<weaponmodifires>().showbuffs();
                    break;
                case "chest":
                    playermov.chest = hit.transform.GetComponent<IInteractible>();
                    break;
                case "portalLevel":
                    playermov.lookingatportal = true;
                    break;
            }
            playermov.inRangeOfInteractible = true;
            raycastelseenabler = true;
        }else if(raycastelseenabler==true){ //didnt hit
            playermov.inRangeOfInteractible = false;
            playermov.chest = null;
            raycastelseenabler = false;
            playermov.lookingatportal = false;
            if(playermov.weaponpicked!=null){
                playermov.weaponpicked.GetComponent<weaponmodifires>().hidebuffs();
            }
        }
        StartCoroutine(a());
    }
}
