using UnityEngine;

public class CollectDoubleCoin : MonoBehaviour
{
    public AudioSource coinFX; //�Ҹ�ȿ��



    private void OnTriggerEnter(Collider other)
    {

       // coinFX.Play();
        // CollectableControl.coinCount += 1;
        GameDataManager.AddCoins(12);
        GameSharedUI.Instance.UpdateCoinsUIText();
        this.gameObject.SetActive(false); //���� ��������ϱ�

    }
}
