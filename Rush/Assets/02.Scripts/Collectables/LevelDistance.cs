using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    //거리UI
    public GameObject disDisplay;
    public int disRun;
    public bool addingDis = false;
    public float disDelay = 0.35f; //1m를 0.35초마다 돌파.....?

    void Update()
    {
        if(addingDis ==false)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }

    }

    //m마다 거리추가
    IEnumerator AddingDis()
    {
        disRun += 1;
        disDisplay.GetComponent<Text>().text = "" + disRun; //거리 텍스트 추가
        yield return new WaitForSeconds(disDelay);
        addingDis=false;
    }
}
