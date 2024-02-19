using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class generatemap : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject ending, newroom ,newdoor;
    public int roomcount = 0, newnumber,lastnumber, leftcount = 0, rightcount = 0;

    public bool[] right,straight;
    public bool lastright = false;

    public NavMeshSurface surface;

    public void Start(){
        
        generate();
    }

    public void generate(){
        for(int i = 0; i<3;i++){
            newnumber = Random.Range(0, rooms.Length);
            if(straight[newnumber]==false){
                if(lastright == false){
                while(right[newnumber]!=true){
                    newnumber = Random.Range(0, rooms.Length);
                }
                lastright = true;
                }else{
                while(right[newnumber]!=false){
                    newnumber = Random.Range(0, rooms.Length);
                }
                lastright = false;
                }
            }
            GameObject a = Instantiate(rooms[newnumber], this.transform.position, this.transform.rotation);

            newroom = a;

            newroom.transform.localScale = new Vector3(1,1,1);

            newroom.transform.parent = newdoor.transform;

            newroom.transform.rotation = newdoor.transform.rotation;

            newdoor = newroom.transform.GetChild(0).GetComponent<mapdoorcontroller>().door[Random.Range(0, newroom.transform.GetChild(0).GetComponent<mapdoorcontroller>().door.Length)];

            newroom.transform.localPosition = new Vector3(0,0,0);
 
            lastnumber = newnumber;
        }

        surface.BuildNavMesh();

        Instantiate(ending, newdoor.transform.position, newdoor.transform.rotation);

        return;
    }
}
