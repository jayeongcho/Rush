using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_coin : MonoBehaviour
{
    public int coin =100;
    // Start is called before the first frame update
   public void getcoin()
    {
        Debug.Log("Getcoin");
        GameDataManager.AddCoins(coin);
        GameSharedUI.Instance.UpdateCoinsUIText();
    }

}
