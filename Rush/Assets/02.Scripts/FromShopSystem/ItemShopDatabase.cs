using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject Ŭ������ ��ӹ޾� ĳ���� ���� �����ͺ��̽��� ��Ÿ���� Ŭ�����Դϴ�.
[CreateAssetMenu(fileName = "ItemShopDatabase", menuName = "Shopping/Items Shop")]


public class ItemShopDatabase : ScriptableObject
{
    //�����۵��� �迭 ���� ����
    public SKillItem [] items;

    //�������� ������ ��ȯ
    public int ItemsCount
    {
        get { return items.Length; }
    }

    //������ �ε����� �������� ��ȯ�ϴ� �޼���
    public SKillItem GetItem(int index)
    {
        return items[index];
    }

    //������ �ε����� �����۸� ������ ���·� �����ϴ� �޼���
    public void PurchaseItem(int index)
    {
        items[index].isPurchased = true;
    }

   
}
