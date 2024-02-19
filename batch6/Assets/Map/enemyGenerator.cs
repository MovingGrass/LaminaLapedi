using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject[] doors;
    public Transform[] EnemySpawnpoint;
    public Collider detection;
    public int numberofspawn;
    public bool spawned = false, alldead = false;
    public List<GameObject> spawnedenemy1 = new List<GameObject>();
    void Start(){
        numberofspawn = Random.Range(2,6);
    }
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Player" && spawned == false){
            generateenemyspawn();
        }
    }
    public void generateenemyspawn(){
        detection.enabled = false;
        spawned = true;
        for(int i = 0;i<doors.Length;i++){
            doors[i].SetActive(true);
        }
        for(int i = 0;i<numberofspawn;i++){
            int rnd = Random.Range(0,EnemySpawnpoint.Length);
            spawnedenemy1.Add(Instantiate(enemy[Random.Range(0,enemy.Length)], EnemySpawnpoint[rnd].position, EnemySpawnpoint[rnd].rotation));
        }
        InvokeRepeating("repeat",0.2f,0.2f);
    }
    void repeat(){
        spawnedenemy1.RemoveAll(item => item == null);
        if(spawnedenemy1.Count == 0 && alldead == false){
            reset();
            StartCoroutine(disablee());
        }
    }
    public void reset(){ //spawned false ada di playermovement!
        CancelInvoke("repeat");
        for(int i = 0;i<doors.Length;i++){
            doors[i].SetActive(false);
        }
        foreach(GameObject objects in spawnedenemy1)
        {
            Destroy(objects);
        }
        spawnedenemy1.Clear();
    }
    IEnumerator disablee(){
        alldead = true;
        yield return new WaitForSeconds(2);
        this.enabled = false;
    }
}
