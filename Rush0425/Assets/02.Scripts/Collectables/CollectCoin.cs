using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinFX; //�Ҹ�ȿ��
    [SerializeField] ItemShopDatabase itemsDB;

    [SerializeField] GameObject itemObj;
   // private bool isProcessing = false; // ó�� ������ ���θ� ��Ÿ���� �÷���

  
    private void OnTriggerEnter(Collider other)
    { 

        if (AreAllChildrenActive()) //�ڽ��� Ȱ��ȭ �� ���
        {
            //�����ۿ� ����Ǿ��ִ� ���� ��������
            SKillItem skillitem = GameDataManager.GetSelectedItem();
            int selectItem = GameDataManager.GetSelectedItemIndex();


            coinFX.Play();
            // CollectableControl.coinCount += 1;
            coinFX.Play();
            Debug.Log("skillitem.getcoin" + skillitem.doblecoin);
            GameDataManager.AddCoins(skillitem.doblecoin);  //������ �����ۺ��� ���� ���� �ٲ�

            GameSharedUI.Instance.UpdateCoinsUIText();
            this.gameObject.SetActive(false);
        }

            else
        {
            coinFX.Play();
            // collectablecontrol.coincount += 1;

            GameDataManager.AddCoins(10);
            GameSharedUI.Instance.UpdateCoinsUIText();
            this.gameObject.SetActive(false); //���� ��������ϱ�
        }





        //coinFX.Play();
        //// CollectableControl.coinCount += 1;
        //coinFX.Play();
        //Debug.Log("skillitem.getcoin"+skillitem.getcoin);
        //GameDataManager.AddCoins(skillitem.getcoin);  //������ �����ۺ��� ���� ���� �ٲ�

        //GameSharedUI.Instance.UpdateCoinsUIText();
        //this.gameObject.SetActive(false);

    }

    private bool AreAllChildrenActive()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf) // �ڽ��� Ȱ��ȭ�Ǿ� ���� ���� ���
            {
                return false; // ��� �ڽ��� Ȱ��ȭ�Ǿ� ���� ����
            }
        }
        return true; // ��� �ڽ��� Ȱ��ȭ�Ǿ� ����
    }
}
    
