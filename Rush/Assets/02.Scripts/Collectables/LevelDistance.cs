using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    //�Ÿ�UI
    public GameObject disDisplay;
    public int disRun;
    public bool addingDis = false;
    public float disDelay = 0.35f; //1m�� 0.35�ʸ��� ����.....?

    void Update()
    {
        if(addingDis ==false)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }

    }

    //m���� �Ÿ��߰�
    IEnumerator AddingDis()
    {
        disRun += 1;
        disDisplay.GetComponent<Text>().text = "" + disRun; //�Ÿ� �ؽ�Ʈ �߰�
        yield return new WaitForSeconds(disDelay);
        addingDis=false;
    }
}
