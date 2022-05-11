using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public GameObject uwuPrefab;
    public GameObject owoPrefab;
    
    CheckpointsSystem checkpointsSystem;

    Vector3 uwuCheckpoint;
    Vector3 owoCheckpoint;


    private void Awake() {
        checkpointsSystem = GetComponentInParent<CheckpointsSystem>();
        uwuCheckpoint = this.transform.GetChild(0).position;
        owoCheckpoint = this.transform.GetChild(1).position;
    }
    private void Update() {
        if(this.transform.childCount==0){
            checkpointsSystem.SetCheckpoint(uwuCheckpoint, owoCheckpoint, this.transform.position/*camera*/);
            Destroy(this.gameObject);
        }       
    }

    public void ReloadScene(){
        for (int i = 0; i < this.transform.childCount; i++)
            Destroy(this.transform.GetChild(i).gameObject);//Destruye anteriores hijos 
        var _owo = Instantiate(owoPrefab,owoCheckpoint,Quaternion.Euler(Vector3.zero), this.transform);
        //instancia nuevos
        var _uwu = Instantiate(uwuPrefab,uwuCheckpoint,Quaternion.Euler(Vector3.zero),this.transform);
    }
}
