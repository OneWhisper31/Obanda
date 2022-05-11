using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsCamera : MonoBehaviour
{

    CheckpointsSystem checkpointsSystem;
    //cada vez que inicia el juego, resetea los spawnpoints
    private void Start() {
        FindObjectOfType<CheckpointsSystem>().CheckForChildren();
    }
}
