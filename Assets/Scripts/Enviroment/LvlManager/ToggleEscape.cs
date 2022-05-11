using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEscape : MonoBehaviour
{
    public GameObject escape;
    bool active;
    // private void Start() {
    //     escape=GetComponentInChildren<RectTransform>().gameObject;
    //     escape.SetActive(false);
    // }
    void Update(){
        if(Input.GetButtonDown("Escape")){
            escape.SetActive(!active);
            active=!active;
        }
    }
}
