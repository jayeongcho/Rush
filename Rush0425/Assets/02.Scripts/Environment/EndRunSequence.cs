using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���� ������ ȭ�鿡 ǥ��

public class EndRunSequence : MonoBehaviour
{
    //������� ����&�Ÿ�
    public GameObject liveCoins;
   // public GameObject liveDis;

    
    public GameObject endScreen;
    public GameObject fadeOut;


    void Start()
    {
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        //��������� ���������
        GameDataManager.AddCoins(CollectableControl.coinCount);
        Debug.Log("GameDataManager.GetCoins()" + GameDataManager.GetCoins());
        Debug.Log("CollectableControl.coinCount" + CollectableControl.coinCount);
       
        
        yield return new WaitForSeconds(3);
        //ȭ�鿡 ǥ������ ����,�Ÿ� ����
                    
        liveCoins.SetActive(false);
       // liveDis.SetActive(false);
        endScreen.SetActive(true);

        //fadeout����
        yield return new WaitForSeconds(3);
       // fadeOut.SetActive(true);

        //�� ��ȯ(MainMenu)
       // yield return new WaitForSeconds(2);
       // SceneManager.LoadScene(1); //����ȭ������ ��ȯ

    }
}
