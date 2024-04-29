using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableControl : MonoBehaviour
{
    public static int coinCount;
    public GameObject coinCountDisplay;
    public GameObject coinEndDisplay;

    void Update()
    {
        //UI 코인 표시
        coinCountDisplay.GetComponent<Text>().text = "" + coinCount;
        coinEndDisplay.GetComponent<Text>().text = "" + coinCount;
    }
}
