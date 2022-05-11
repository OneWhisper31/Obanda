using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwOHabilities : MonoBehaviour
{

    public float checkGround;
    public float pushAmt;
    public float tpCooldown;
    public LayerMask whatIsGround;
    public UwUHabilites uwu;
    public MultipleTargetCamera targetCamera;
	
    public AudioClip teleport;
    public AudioClip gravity;

    Rigidbody2D rb;
    BoxCollider2D col;
    Animator anim;
    SpriteRenderer sprite;
    BasicMovement movement;


    bool isTeleporting;
    bool isGrounded;
    bool isFlipingGravity;

    float currentGravity;
    float coold;
    
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        movement = GetComponentInParent<BasicMovement>();
        col = GetComponentInParent<BoxCollider2D>();
    }
    void FixedUpdate(){
        movement.Movement();
        FixedTeleport();
    } 
    void FixedTeleport(){
        if(isTeleporting){

            if(!sprite.flipX) rb.AddForce(pushAmt*100*Vector2.right);
            else rb.AddForce(-pushAmt*100*Vector2.right);

            isTeleporting=false;
        }
    }
    void Update()
    {
        movement.AnimManager();
        Teleport();
        FlipGravity();
    }

    void Teleport(){
        if(Input.GetButtonDown("Vertical 2")&&Input.GetAxisRaw("Vertical 2")==1&&coold<=0/*&&movement.distance<.1f*/){
            //si esta apretando la tecla para arriba, no tiene cooldown y esta cerca de el piso
            movement.audioSource.PlayOneShot(teleport);
            movement.audioSource.volume = .7f;
            movement.audioSource.pitch = .9f;
            isTeleporting = true;
            coold = tpCooldown;
            anim.SetTrigger("Teleport");
            //StartCoroutine("Smooth");
        }
        else if(coold>=0) coold-=Time.deltaTime;
    }
    void FlipGravity(){
        if(movement.distance<=.1f&&isFlipingGravity){
            Physics2D.gravity = currentGravity*Vector2.up;
            StopAllCoroutines();
            isFlipingGravity=false;
        }
        else if(Input.GetButtonDown("Vertical 2")&&Input.GetAxisRaw("Vertical 2")==-1&&movement.distance<=.1f){
            currentGravity =  Physics2D.gravity.y*-1;
            StopAllCoroutines();
            StartCoroutine(FlipGravityLerp (Physics2D.gravity*-1));
            movement.audioSource.PlayOneShot(gravity);
            movement.audioSource.volume = .9f;
            movement.audioSource.pitch = 1;
            isFlipingGravity=true;
            Collider2D[] owocol = new Collider2D[] {col};
            Collider2D[] uwucols = new Collider2D[] {uwu.normalCol,uwu.slideCol};
            //manda a flipear los characters
            Flip(sprite, movement.feetL,movement.feetR, owocol);
            var _uwu = uwu.movement;
            Flip(uwu.sprite, _uwu.feetL,_uwu.feetR,uwucols);
        }
    }
    void Flip(SpriteRenderer _sprite, Transform _feetL,Transform _feetR, Collider2D[] colls){
        _sprite.flipY = !_sprite.flipY;//da vuelta el sprite
        _feetL.localPosition = _feetL.localPosition*-1;//cambia la localizacion imaginaria de los pies
        _feetR.localPosition = _feetR.localPosition*-1;//cambia la localizacion imaginaria de los pies
        foreach (Collider2D _col in colls)//da vuelta los coliders
            _col.offset = new Vector2(_col.offset.x,_col.offset.y*-1);   
        if(!_sprite.flipY)_feetL.rotation = Quaternion.Euler(Vector3.zero);
        //si no esta invertido la rotacion de los pies es 0
        else if (_sprite.flipY)_feetL.rotation = Quaternion.Euler(180*Vector3.forward);
        //si esta invertido la rotacion es 180;
        if(!_sprite.flipY)_feetR.rotation = Quaternion.Euler(Vector3.zero);
        //si no esta invertido la rotacion de los pies es 0
        else if (_sprite.flipY)_feetR.rotation = Quaternion.Euler(180*Vector3.forward);
        //si esta invertido la rotacion es 180;
        //esto es para que el rayo se envie en la direccion correcta       
    }
    IEnumerator FlipGravityLerp(Vector2 b){
        Physics2D.gravity=0*Vector2.up;
        for (; Mathf.Abs(Physics2D.gravity.y+ b.y)<=Mathf.Abs(b.y*2)-Mathf.Abs(.2f);)
        //mientras que gravity no se acerque a b se siga ejecutando
        {
            Physics2D.gravity = Vector2.Lerp(Physics2D.gravity,b,Mathf.Max(.4f,movement.distance/10));
            yield return new WaitForSeconds(.1f);
        }
        Physics2D.gravity=b;
        isFlipingGravity= false;
        yield break;
    }
}
