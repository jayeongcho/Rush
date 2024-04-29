using UnityEngine;

public class CollectDoubleCoin : MonoBehaviour
{
    public AudioSource coinFX; //소리효과



    private void OnTriggerEnter(Collider other)
    {

       // coinFX.Play();
        // CollectableControl.coinCount += 1;
        GameDataManager.AddCoins(12);
        GameSharedUI.Instance.UpdateCoinsUIText();
        this.gameObject.SetActive(false); //코인 사라지게하기

    }
}
