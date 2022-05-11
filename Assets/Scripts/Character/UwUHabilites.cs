using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UwUHabilites : MonoBehaviour
{
    public float jumpForce;
    public float jumpCounter;
    public float checkGround;
    public float slideImpulse;
    public float slideTimer;

    public LayerMask whatIsGround;
	
    public BoxCollider2D normalCol;
    public BoxCollider2D slideCol;

    public AudioClip jump;    
    public AudioClip slide;

    [HideInInspector]
    public SpriteRenderer sprite;

    [HideInInspector]
    public BasicMovement movement;
    [HideInInspector]
    public Rigidbody2D rb;
    Animator anim;

    float coold;
    float sTimer;

    bool canSlide;
    bool isSliding;
    bool isJumping;
    float Jtimer;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<BasicMovement>();
    }
    void FixedUpdate(){
        movement.Movement();
        FixedJump();
        if(isSliding) FixedSlide();
    }
    void Update(){
        movement.AnimManager();
        Jump();
        Slide();
    }

    void FixedJump(){
        if(isJumping&&Jtimer>0){//si esta saltando y el timer lo deja, que se aplique aceleracion
            if(sprite.flipY) rb.velocity -= jumpForce*Vector2.up*Time.deltaTime;
            else rb.velocity += jumpForce*Vector2.up*Time.deltaTime;
            Jtimer-=Time.deltaTime;
        }
        else isJumping=false;
    }
    void Jump(){
        if(movement.distance<=.1f&& Input.GetButtonDown("Vertical 1")&&Input.GetAxisRaw("Vertical 1")==1&&!isJumping){
            //si esta apretando la tecla en positivo y esta en el piso, que salte
            isJumping=true; Jtimer = jumpCounter; rb.gravityScale = 0;
            movement.audioSource.PlayOneShot(jump);
            movement.audioSource.volume=.6f;
        }
        else if(!isJumping||(Input.GetAxisRaw("Vertical 1")==0&&Jtimer<=jumpCounter/2)){ rb.gravityScale = 5; isJumping=false;}
        //si dejo de saltar y esta empezando a caer que le devuelvan la gravedad
        anim.SetBool("isJumping",isJumping);
    }
    void FixedSlide(){
        if(isSliding){
            if(!sprite.flipX) rb.velocity+= transform.right.x * slideImpulse*Time.deltaTime*Vector2.right;
            else rb.velocity -= transform.right.x * slideImpulse*Time.deltaTime*Vector2.right;
        }
    }
    void Slide(){
        if((rb.velocity.x >= movement.speed-.3f&&!sprite.flipX)||
            (rb.velocity.x <= -movement.speed+.3f&&sprite.flipX)) 
            canSlide = true;
        else
            canSlide = false;
        if(Input.GetButtonDown("Vertical 1")&&Input.GetAxisRaw("Vertical 1")==-1
            &&!isSliding&&!isJumping&&movement.distance<.1f&&canSlide){
            isSliding = true;
            anim.SetBool("isSliding",isSliding);
        }
        else if ((Input.GetButtonUp("Vertical 1")||sTimer<=0)&&isSliding){
            isSliding = false;
            anim.SetBool("isSliding",isSliding);
        }
        if(sTimer>0) sTimer -= Time.deltaTime;
    }
    void StartSlide(){//se ejecuta cuando el slide ya se esta haciendo
        movement.audioSource.PlayOneShot(slide);
        movement.audioSource.volume=.3f;
        canSlide = false;
        normalCol.enabled = false;
        slideCol.enabled = true;
        movement.currentspeed = 0;
        //movement.enabled = false;
        sTimer = slideTimer;
    }
    void FirstFrameFinishSlide(){
        rb.velocity = new Vector2(0, rb.velocity.y);
        normalCol.enabled = true;
        slideCol.enabled = false;
    }
    void FinishSlide(){//se ejcuta cuando el slide termina
        movement.currentspeed = movement.speed;
        //movement.enabled = true;
        //sTimer = 0;
    }
}
