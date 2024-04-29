using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject Ŭ������ ��ӹ޾� ĳ���� ���� �����ͺ��̽��� ��Ÿ���� Ŭ�����Դϴ�.
[CreateAssetMenu(fileName ="CharacterShopDatabase", menuName ="Shopping/Characters Shop")]
public class CharacterShopDatabase : ScriptableObject
{
    //ĳ���͵��� �迭 ���� ����
    public Character[] characters;

    //ĳ������ ������ ��ȯ
    public int CharactersCount
    {
        get { return characters.Length; }
    }

    //������ �ε����� ĳ���͸� ��ȯ�ϴ� �޼���
    public Character GetCharacter(int index)
    {
        return characters[index];
    }

    //������ �ε����� ĳ���͸� ������ ���·� �����ϴ� �޼���
    public void PurchaseCharacter(int index)
    {
        characters[index].isPurchased = true;
    }

    
}
