using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public character Player;
    public float speed;

    
    public Transform SpritePivot;
    public Transform feetL;
    public Transform feetR;

    [Tooltip("Valor 0 pivot, valor 1 feetL, Valor 2 feetR")]
    public float[] precisePositions;


    [HideInInspector]
    public float distance;
    [HideInInspector]
    public float currentspeed;
    public LayerMask whatIsGround;


    public SpriteRenderer sprite;
    [HideInInspector]
    public Animator anim; 
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public string horizontal;
    [HideInInspector]
    public AudioSource audioSource;

    void Start()
    {
        currentspeed=speed;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        horizontal = Player==character.Player1?"Horizontal 1":"Horizontal 2";
    }
    public void Movement(){        
        rb.velocity = new Vector2(Input.GetAxis(horizontal)*speed,rb.velocity.y); //si aprieta tecla que se mueva
        if(rb.velocity.x<0) sprite.flipX = true;//si se mueve para la derecha que rote
        else if(rb.velocity.x>0) sprite.flipX = false;//sino que mire bien
        if(Input.GetAxisRaw(horizontal)==1)
            anim.SetFloat("Velocity", Mathf.Max(.3f,Input.GetAxis(horizontal)));
        //para q tenga un minimo movimiento cuando se apreta poco
        else if(Input.GetAxisRaw(horizontal)==-1)
            anim.SetFloat("Velocity", Mathf.Min(-.3f,Input.GetAxis(horizontal)));
        else
            anim.SetFloat("Velocity", Input.GetAxis(horizontal));
    }
    public void AnimManager(){
        if(sprite.flipX){//si esta invertido
            SpritePivot.localPosition=precisePositions[0]*Vector3.right;//colider ajustado
            feetL.localPosition=precisePositions[1]*Vector3.right+feetL.localPosition.y*Vector3.up;
            //pies ajustados
            feetR.localPosition=precisePositions[2]*Vector3.right+feetR.localPosition.y*Vector3.up;
        }//si se mueve para la derecha que rote
        else if(!sprite.flipX){ //sino esta invertido
            SpritePivot.localPosition=Vector3.zero;//colider mantiene posicion
            feetL.localPosition=-precisePositions[2]*Vector3.right+feetL.localPosition.y*Vector3.up;
            //pies vuelven a pos original, es el negativo del otro pie invertido
            feetR.localPosition=-precisePositions[1]*Vector3.right+feetR.localPosition.y*Vector3.up;
        }//sino que mire bien
        RaycastHit2D raycast = Physics2D.Raycast(feetL.position ,-feetL.transform.up,Mathf.Infinity,whatIsGround);
        distance = Vector2.Distance(feetL.position.y*Vector2.up,raycast.point.y*Vector2.up);
        //si pie izquierdo esta lejos, comprobar el otro pie
        if(distance>.2f){
            raycast = Physics2D.Raycast(feetR.position ,-feetR.transform.up,Mathf.Infinity,whatIsGround);
            distance = Vector2.Distance(feetR.position.y*Vector2.up,raycast.point.y*Vector2.up);
        }
        anim.SetFloat("GroundDistance", distance);
        //laza un rayo desde sus pies hasta encontrar un piso, luego se calcula cuan lejos esta del piso
    }
}
public enum character{
    Player1,
    Player2,
    FinishLevel
}
