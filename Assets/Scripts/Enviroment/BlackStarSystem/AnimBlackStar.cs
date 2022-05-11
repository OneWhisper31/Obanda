using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBlackStar : MonoBehaviour
{
    public void Destroy(){//para que destuya en el ultimo frame la blackstar
        Destroy(this.gameObject);
    }
}
