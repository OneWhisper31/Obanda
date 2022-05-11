using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class MouseSelected : MonoBehaviour
{
    public string button;
    public BoxCollider2D coll;
    public LvlManager lvl;
    public Animator credits;

    bool alreadyPressed;
    GameObject checkPointsSystem;
    
    private void Start() {
        checkPointsSystem=GameObject.Find("CheckPointsSystem");
        lvl=FindObjectOfType<LvlManager>(true);
    }
    void Update(){
        if(Input.GetButtonDown("Space")&&button=="Start")
            //si toca el espacio y es start se ejecuta la animacion
            this.GetComponent<Button>().onClick.Invoke();
    }
    public void Button(){//Ejecutado por el onclick, ejecuta animacion
        StartCoroutine(PressedButton());
    }
    IEnumerator PressedButton(){
        BoxCollider2D coll = this.GetComponent<BoxCollider2D>();
        this.GetComponent<Image>().color=new Color(128,128,128,255);
        this.gameObject.transform.localScale=.9f*Vector3.one;
        coll.size=220*Vector2.right+110*Vector2.up;
        //efectos
        yield return new WaitForSecondsRealtime(.1f);
        if(button=="Start"&&!alreadyPressed){
            //si es start y es la primera vez q se toca, se cambia al lvl 1
            alreadyPressed=true;
            lvl.enabled = true;
        }
        else if(button=="Credits"&&!alreadyPressed)
            credits.SetBool("Credits", !credits.GetBool("Credits"));
        else if(button=="Exit"&&!alreadyPressed){
            alreadyPressed=true;
            Destroy(checkPointsSystem);
            //le dice q lo mande al menu
            lvl.currentScene=3;
            lvl.enabled=true;
        }
        this.GetComponent<Image>().color=new Color(255,255,255,255);
        this.gameObject.transform.localScale=Vector3.one;
        coll.size=180*Vector2.right+90*Vector2.up;
        //revierte el efecto
    }
    private void OnMouseEnter() {//si pasas el mouse por arriba
        this.gameObject.transform.localScale=.9f*Vector3.one;
        coll.size=220*Vector2.right+110*Vector2.up;
    }
    private void OnMouseExit() {//si sacas el mouse del lugar
        this.gameObject.transform.localScale=Vector3.one;
        coll.size=180*Vector2.right+90*Vector2.up;
    }
}
