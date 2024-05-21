using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임 끝나고 화면에 표시

public class EndRunSequence : MonoBehaviour
{
    //현재상태 코인&거리
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
        //게임종료시 코인저장됨
        GameDataManager.AddCoins(CollectableControl.coinCount);
        Debug.Log("GameDataManager.GetCoins()" + GameDataManager.GetCoins());
        Debug.Log("CollectableControl.coinCount" + CollectableControl.coinCount);
       
        
        yield return new WaitForSeconds(3);
        //화면에 표시중인 코인,거리 끄기
                    
        liveCoins.SetActive(false);
       // liveDis.SetActive(false);
        endScreen.SetActive(true);

        //fadeout실행
        yield return new WaitForSeconds(3);
       // fadeOut.SetActive(true);

        //씬 전환(MainMenu)
       // yield return new WaitForSeconds(2);
       // SceneManager.LoadScene(1); //레벨화면으로 전환

    }
}
