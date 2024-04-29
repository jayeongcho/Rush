
using UnityEngine;

[System.Serializable] public struct SKillItem
{
    public Sprite image;
    public string name;
    public string info;
    public int getcoin;//���� 2��
    public float speed;//���ǵ�
    public int char_size; //ĳ���� ������

    public int price;


    public bool respawn;//��Ȱ
    public bool isPurchased;//���� ���� Ȯ��

    public GameObject itemObject;
}


