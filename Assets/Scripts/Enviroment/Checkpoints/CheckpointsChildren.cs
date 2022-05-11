using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsChildren : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player")
            Destroy(this.gameObject);
        //si el player entra en el checkpoint, destruirse el padre cuenta y 
        //si ve que no tiene hijos actualiza checkpoint
    }
}
