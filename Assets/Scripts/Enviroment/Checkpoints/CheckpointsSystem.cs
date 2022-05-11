using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointsSystem : MonoBehaviour
{
    Vector3 uwuCheckpoint;
    Vector3 owoCheckpoint;
    Vector3 cameraCheckpoint;

    Scene scene;
    void Awake()
    {
        var checkSystem = FindObjectOfType<CheckpointsSystem>().gameObject;
        if(checkSystem&&checkSystem!=this.gameObject){
            Destroy(this.gameObject);//metodo para evitar que se duplique el checkpoint system
            return;
        }
        else
            DontDestroyOnLoad(this.gameObject);  
    }
    private void Update() {
        if(SceneManager.GetActiveScene()!=scene&&uwuCheckpoint!=Vector3.zero&&owoCheckpoint!=Vector3.zero)
            ApplyCheckpoint();//si cambio de scena, que aplique el checkpoint
        else
            scene = SceneManager.GetActiveScene();
    }
    public void SetCheckpoint(Vector3 uwu, Vector3 owo, Vector3 camera){
        //actualiza internamente el checkpoint
        uwuCheckpoint = uwu;
        owoCheckpoint = owo;
        //mueve la camara
        cameraCheckpoint= camera;
    }
    void ApplyCheckpoint(){
        //aplica luego de recargar la escena los checkpoints
        GameObject.Find("Character 1 - UwU").transform.position=uwuCheckpoint;
        //nuevos character les aplica la posicion
        GameObject.Find("Character 1 - OwO").transform.position=owoCheckpoint;
        GameObject.Find("Main Camera").transform.position=cameraCheckpoint;
        scene = SceneManager.GetActiveScene();
        //le dice que cambio de escena devuelta
    }
    public void CheckForChildren(){//llamado por cada vez que la escena se resetea por un script en Cameras
        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).GetComponent<Checkpoints>().ReloadScene();
    }
}
