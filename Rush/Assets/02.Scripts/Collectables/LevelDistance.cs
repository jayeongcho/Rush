using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    //거리UI
    public GameObject disDisplay;
    public GameObject disEndDisplay;
    public int disRun;
    public bool addingDis = false;
    public float disDelay = 0.35f; //1m를 0.35초마다 돌파

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
        disDisplay.GetComponent<Text>().text = "" + disRun; //게임 중 거리 텍스트 
        disEndDisplay.GetComponent<Text>().text = "" + disRun; //게임 끝나고 나오는 텍스트
        yield return new WaitForSeconds(disDelay);
        addingDis=false;
    }
}
