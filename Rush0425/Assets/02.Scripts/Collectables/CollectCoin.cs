using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinFX; //소리효과
    [SerializeField] ItemShopDatabase itemsDB;

    [SerializeField] GameObject itemObj;
   // private bool isProcessing = false; // 처리 중인지 여부를 나타내는 플래그

  
    private void OnTriggerEnter(Collider other)
    { 

        if (AreAllChildrenActive()) //자식이 활성화 된 경우
        {
            //아이템에 저장되어있는 값을 가져오기
            SKillItem skillitem = GameDataManager.GetSelectedItem();
            int selectItem = GameDataManager.GetSelectedItemIndex();


            coinFX.Play();
            // CollectableControl.coinCount += 1;
            coinFX.Play();
            Debug.Log("skillitem.getcoin" + skillitem.doblecoin);
            GameDataManager.AddCoins(skillitem.doblecoin);  //선택한 아이템별로 코인 값이 바뀜

            GameSharedUI.Instance.UpdateCoinsUIText();
            this.gameObject.SetActive(false);
        }

            else
        {
            coinFX.Play();
            // collectablecontrol.coincount += 1;

            GameDataManager.AddCoins(10);
            GameSharedUI.Instance.UpdateCoinsUIText();
            this.gameObject.SetActive(false); //코인 사라지게하기
        }





        //coinFX.Play();
        //// CollectableControl.coinCount += 1;
        //coinFX.Play();
        //Debug.Log("skillitem.getcoin"+skillitem.getcoin);
        //GameDataManager.AddCoins(skillitem.getcoin);  //선택한 아이템별로 코인 값이 바뀜

        //GameSharedUI.Instance.UpdateCoinsUIText();
        //this.gameObject.SetActive(false);

    }

    private bool AreAllChildrenActive()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf) // 자식이 활성화되어 있지 않은 경우
            {
                return false; // 모든 자식이 활성화되어 있지 않음
            }
        }
        return true; // 모든 자식이 활성화되어 있음
    }
}
    
