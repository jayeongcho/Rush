using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject 클래스를 상속받아 캐릭터 상점 데이터베이스를 나타내는 클래스입니다.
[CreateAssetMenu(fileName ="CharacterShopDatabase", menuName ="Shopping/Characters Shop")]
public class CharacterShopDatabase : ScriptableObject
{
    //캐릭터들의 배열 저장 변수
    public Character[] characters;

    //캐릭터의 개수를 반환
    public int CharactersCount
    {
        get { return characters.Length; }
    }

    //지정된 인덱스의 캐릭터를 반환하는 메서드
    public Character GetCharacter(int index)
    {
        return characters[index];
    }

    //지정된 인덱스의 캐릭터를 구매한 상태로 설정하는 메서드
    public void PurchaseCharacter(int index)
    {
        characters[index].isPurchased = true;
    }

    
}
