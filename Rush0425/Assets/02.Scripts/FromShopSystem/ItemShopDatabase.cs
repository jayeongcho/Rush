using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject 클래스를 상속받아 캐릭터 상점 데이터베이스를 나타내는 클래스입니다.
[CreateAssetMenu(fileName = "ItemShopDatabase", menuName = "Shopping/Items Shop")]


public class ItemShopDatabase : ScriptableObject
{
    //아이템들의 배열 저장 변수
    public SKillItem [] items;

    //아이템의 개수를 반환
    public int ItemsCount
    {
        get { return items.Length; }
    }

    //지정된 인덱스의 아이템을 반환하는 메서드
    public SKillItem GetItem(int index)
    {
        return items[index];
    }

    //지정된 인덱스의 아이템를 구매한 상태로 설정하는 메서드
    public void PurchaseItem(int index)
    {
        items[index].isPurchased = true;
    }

   
}
