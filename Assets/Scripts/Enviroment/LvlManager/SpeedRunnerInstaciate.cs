using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRunnerInstaciate : MonoBehaviour
{
    public GameObject sing;
    public GameObject prefab;
    public void Unlocked(){
        StartCoroutine(UnlockedCo());
    }
    private void Start() {
        if(FindObjectOfType<SpeedRunnerCounter>()) Destroy(this.gameObject);
    }
    IEnumerator UnlockedCo(){
        sing.SetActive(true);
        var _prefab = Instantiate(prefab,Vector3.zero,Quaternion.Euler(Vector3.zero));
        DontDestroyOnLoad(_prefab);
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(this.gameObject);
        yield break;
    }
}
