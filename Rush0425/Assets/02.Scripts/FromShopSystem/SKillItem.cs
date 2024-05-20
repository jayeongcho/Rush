
using UnityEngine;

[System.Serializable] public struct SKillItem
{
    public Sprite image;
    public string name;
    public string info;
   
    public int doblecoin;//코인 2배
   
    public float speed;//스피드
   
    public int price;


    public bool isrespawn;//부활
    public bool isPurchased;//구매 여부 확인

    public GameObject itemObject;
}


