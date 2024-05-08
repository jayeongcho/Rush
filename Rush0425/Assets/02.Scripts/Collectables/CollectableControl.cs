using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollectableControl : MonoBehaviour
{
    public static int coinCount;
    public TMP_Text coinCountDisplay;
    public TMP_Text coinEndDisplay;
   
    private void Start()
    {
        coinCount = 0;
       
    }
    void Update()
    {
        //UI 코인 표시
        // coinCountDisplay.GetComponent<TextMeshPro>().text = "" + coinCount;
        //  coinEndDisplay.GetComponent<TextMeshPro>().text = "" + coinCount;
        coinCountDisplay.text = coinCount.ToString();
        coinEndDisplay.text = coinCount.ToString();


    }

    
}
