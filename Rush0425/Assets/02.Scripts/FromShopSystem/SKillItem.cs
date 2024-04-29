
using UnityEngine;

[System.Serializable] public struct SKillItem
{
    public Sprite image;
    public string name;
    public string info;
    public int getcoin;//코인 2배
    public float speed;//스피드
    public int char_size; //캐릭터 사이즈

    public int price;


    public bool respawn;//부활
    public bool isPurchased;//구매 여부 확인

    public GameObject itemObject;
}


