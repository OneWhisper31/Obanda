using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelChecker : MonoBehaviour
{
    //script encargado de checkear si los dos estan en el final

    public LvlManager lvl;
    public BoxCollider2D[] walls;
    public MultipleTargetCamera targetCamera;
    
    public GameObject finalLevel;
    
    BasicMovement uwuMov;
    UwUHabilites uwuHabilites;
    BasicMovement owoMov;
    OwOHabilities owoHabilites;
    
    GameObject checkPointsSystem;

    Vector2 gravity=Vector2.up*-9.8f;//gravedad standar

    bool isFinishing;

    private void Awake() {
        lvl = GameObject.Find("LvlManager").GetComponent<LvlManager>();
    }

    private void Update() {
        if(uwuMov&owoMov&!isFinishing){
            StartCoroutine(LevelFinish());
            isFinishing =true;
        }
    }
    private void Start() {
        checkPointsSystem=GameObject.Find("CheckPointsSystem");
    }    
    IEnumerator LevelFinish(){
        if(finalLevel){
            lvl.currentScene=-1;
            //asi cuando haga la transicion lo mande al lvl, 
            //tambien sirve para el speedrunercounter si existe
        }
        //mover players para adelante y desactivar controles
        uwuHabilites.rb.gravityScale = 5;
        uwuHabilites.enabled=false;
        owoHabilites.StopAllCoroutines();
        owoHabilites.enabled=false;
        Physics2D.gravity= gravity;
        FinishLevel(uwuMov);
        FinishLevel(owoMov);

        //desactivar pared invisible
        foreach (var wall in walls)
        {
            wall.enabled=false;
        } 
        //rightWall.enabled=false;
        //fijar camara
        targetCamera.enabled=false;
        Destroy(checkPointsSystem);
        //transicion al prox lvl
        yield return new WaitForSecondsRealtime(.3f);
        if(!finalLevel)
            lvl.enabled = true;
        else{
            finalLevel.SetActive(true);
            yield return new WaitForSecondsRealtime(5f);
            lvl.enabled = true;
        }
        yield break;
    }
    public void FinishLevel(BasicMovement mov){//mueve los personajes hacia el horizonte
        mov.rb.velocity = new Vector2(1*mov.speed,mov.rb.velocity.y);
        mov.sprite.flipX = false;
        mov.sprite.flipY = false;
        mov.anim.SetBool("isJumping",false);
        mov.anim.SetBool("isSliding",false);
        mov.anim.SetFloat("GroundDistance",0);
        mov.anim.SetFloat("Velocity", 1);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            if(other.GetComponentInChildren<UwUHabilites>()){
                uwuHabilites=other.GetComponentInChildren<UwUHabilites>();
                uwuMov=other.GetComponent<BasicMovement>();
            }
            else{
                owoHabilites=other.GetComponentInChildren<OwOHabilities>();
                owoMov=other.GetComponent<BasicMovement>();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Player"&!isFinishing){
            if(other.GetComponentInChildren<UwUHabilites>()){
                uwuHabilites=null;
                uwuMov=null;
            }
            else{
                owoHabilites=null;
                owoMov=null;
            }
        }
    }
}
