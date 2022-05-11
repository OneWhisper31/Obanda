using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStar : MonoBehaviour
{
    
    public Animator UnlockFloor;
    Animator thisAnim;
    AudioSource audioSource;
    
    private void Start() {
        thisAnim=GetComponentInChildren<Animator>();
        audioSource=GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            //activa el piso
            UnlockFloor.SetTrigger("Activated");
            //animacion destruir
            audioSource.Play();
            thisAnim.SetTrigger("Destroy");
            //desactiva el trigger
            GetComponent<CircleCollider2D>().enabled=false;
        }
    }
}
