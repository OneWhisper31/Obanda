using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedRunnerCounter : MonoBehaviour
{
    public TextMeshProUGUI singCounter;
    public float miliCounter;
    public float secondCounter=-1;//debido a que empieza cuando hace la cinematica
    
    LvlManager lvl;

    GameObject maincamera;
    private void OnEnable() {
        lvl = FindObjectOfType<LvlManager>();
    }
    void Update()
    {
        copyTrasformCamera();
        if(lvl.currentScene>0&&lvl.speedrunerbool){
            singCounter.gameObject.SetActive(true);
            miliCounter++;
            if(miliCounter>=99){
                miliCounter=0;
                secondCounter++;
            }
            singCounter.SetText(secondCounter+" : "+(miliCounter<10?"0"+miliCounter.ToString():miliCounter.ToString()));
        }
        else if(lvl.currentScene==-1){
            StartCoroutine(Finished());
        }
        else if(lvl.currentScene==0&&lvl.speedrunerbool){
            lvl.speedrunerbool=false;
            singCounter.SetText("0:00");
            miliCounter=0;
            secondCounter=0;
            this.GetComponent<Animator>().SetTrigger("Reset");
            singCounter.gameObject.SetActive(false);
        }
            //no sumes mas
            //resetear dspues de unos segundos
    }
    IEnumerator Finished(){
        this.GetComponent<Animator>().SetTrigger("Finished");
        yield break;
    }
    void copyTrasformCamera(){
        if(maincamera==null&&lvl.currentScene>0){
            maincamera = GameObject.Find("ExitButton");
        }
        else if(maincamera!=null){
            this.transform.position=maincamera.transform.position;
            this.transform.localScale=maincamera.transform.localScale;
        }
    }
}
