using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{
    public int currentScene;

    [HideInInspector]
    public Animator anim;

    lvlManagerCanvas lvlCanvas;
    Vector2 gravity;
    public bool speedrunerbool;
    void Awake(){
        var checkSystem = FindObjectOfType<LvlManager>().gameObject;
        if(checkSystem&&checkSystem!=this.gameObject){
            Destroy(this.gameObject);//metodo para evitar que se duplique el lvlmanager
            return;
        }
        else
            DontDestroyOnLoad(this.gameObject);
        anim=GetComponentInChildren<Animator>();
        lvlCanvas=GetComponentInChildren<lvlManagerCanvas>();
        gravity=Physics2D.gravity;
    }
    private void OnEnable(){
        currentScene++;
        if(currentScene==0) currentScene=4;
        lvlCanvas.sceneToLoad = "Level "+currentScene;
        anim.SetTrigger("Close");
        if(currentScene==4) currentScene=0;
        this.enabled=false;
    }
    public IEnumerator LoadAsyncScene(string sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Physics2D.gravity=gravity;
        anim.SetTrigger("Open");
        speedrunerbool=asyncLoad.isDone;
        yield break;
    }

    //diferentes funciones para cada evento o usar unity events
}
