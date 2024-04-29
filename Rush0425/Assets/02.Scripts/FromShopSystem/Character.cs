
using UnityEngine;


[System.Serializable]
public struct Character
{
    public Sprite image;
    
    public string name;
    [Range(0, 100)] public float speed; //스피드 속성
    [Range(0, 100)] public float power;//힘 속성

    public int price;

    public bool isPurchased;//구매 여부 확인

}