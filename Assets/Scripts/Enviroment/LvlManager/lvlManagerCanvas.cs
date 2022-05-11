using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlManagerCanvas : MonoBehaviour
{

    
    [HideInInspector]
    public string sceneToLoad;
    
    LvlManager lvl;

    private void OnEnable() {
        lvl = GetComponentInParent<LvlManager>();    
    }
    public void NewLvl(){
        StartCoroutine(lvl.LoadAsyncScene(sceneToLoad));
    }
}
