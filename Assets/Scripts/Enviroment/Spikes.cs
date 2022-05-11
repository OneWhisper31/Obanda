using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            var health = other.GetComponentInChildren<Health>();
            health.StartDying();
        }
    }
}
