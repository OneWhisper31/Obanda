using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public BasicMovement[] mov = new BasicMovement[2];
    public OwOHabilities owo; 
    public UwUHabilites uwu;
    public MultipleTargetCamera targetCamera;
    public GameObject destruction;
    [HideInInspector]
    public bool allreadyLoading;

    Animator canvasAnim;
    lvlManagerCanvas lvlCanvas;

    private void Start() {
        var lvlmanager = GameObject.Find("LvlManager");
        canvasAnim= lvlmanager.GetComponent<LvlManager>().anim;
        lvlCanvas = lvlmanager.GetComponentInChildren<lvlManagerCanvas>();
        if(mov[0].gameObject==GameObject.Find("Character 1 - OwO")) return;//para q se ejecute una vez
        canvasAnim.SetTrigger("Open");
    }

    public void StartDying(){
        if(!mov[0].anim.enabled)
            return;
        //instancia explosion si no se ejecuto la funcion
        Instantiate(destruction, mov[0].transform.position, Quaternion.Euler(Vector3.zero));
        mov[0].anim.enabled = false;
        mov[0].sprite.color = new Color(255,255,255,0);
        if(!allreadyLoading){
            allreadyLoading=true;
            mov[1].GetComponentInChildren<Health>().allreadyLoading=true;
            owo.StopAllCoroutines();
            owo.enabled = false;
            //desactiva habilidades
            uwu.rb.gravityScale = 5;
            uwu.enabled = false;
            FinishLevel(mov[0]);
            //saca velocidad
            FinishLevel(mov[1]);
            //desactiva habilidades
            mov[0].enabled = false;
            //desactiva mov
            mov[1].enabled=false;
            //le avisa al otro health que no hace falta cargar la escena
            StartCoroutine(EndTry());
        }
    }
    public void FinishLevel(BasicMovement mov){//mueve los personajes hacia el horizonte
        mov.sprite.flipY=Physics2D.gravity.y>0;//si es mayor a 0 se inverte, sino no
        mov.rb.velocity=mov.rb.velocity.y*Vector2.up;
        mov.anim.SetBool("isJumping",false);
        mov.anim.SetBool("isSliding",false);
        mov.anim.SetFloat("GroundDistance",0);
        mov.anim.SetFloat("Velocity", 0);
    }
    IEnumerator EndTry(){
        lvlCanvas.sceneToLoad = SceneManager.GetActiveScene().name;
        yield return new WaitForSecondsRealtime(.85f);
        canvasAnim.SetTrigger("Close");//activa comienzo de volver a cargar escena
        yield return new WaitForEndOfFrame();
        canvasAnim.ResetTrigger("Close");//activa comienzo de volver a cargar escena
    }
}
