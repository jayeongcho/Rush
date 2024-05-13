using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Shop Data Holder 상점데이터를 보유하는 클래스
[System.Serializable] public class CharactersShopData
{
	// 구매한 캐릭터 인덱스 목록을 저장하는 리스트
	public List<int> purchasedCharactersIndexes = new List<int> ();
}

[System.Serializable] public class ItemsShopData
{
    // 구매한 캐릭터 인덱스 목록을 저장하는 리스트
    public List<int> purchasedItemsIndexes = new List<int>();
}

//추가: 레벨
[System.Serializable]
public class LevelData
{
    public int levelIndex; // 레벨 인덱스
    public bool unlocked; // 레벨이 잠금 해제되었는지 여부
    public int starsCollected; // 획득한 별의 수
    // 필요한 다른 정보들 추가 가능
}

//Player Data Holder 플레이어 데이터를 보유하는 클래스
[System.Serializable] public class PlayerData
{ 
	//보유한 코인 수
	public int coins = 0;
   
    //선택한 캐릭터 인덱스
    public int selectedCharacterIndex = 0;
	public int selectedItemIndex = 0;
 
}

//게임 데이터를 관리하는 정적 클래스
public static class GameDataManager
{
    //플레이어 데이터와 상점 데이터를 저장하는 정적 변수
   
    static PlayerData playerData = new PlayerData ();
	static CharactersShopData charactersShopData= new CharactersShopData ();
    static ItemsShopData ItemsShopData = new ItemsShopData();
	//레벨
	//private static List<LevelData> levelDataList = new List<LevelData>();

    static Character selectedCharacter;
	static SKillItem selectedSKillItem;
	

    //생성자
    static GameDataManager ()
	{
		LoadPlayerData ();
		LoadCharactersShopData ();
	}
   

    //Player Data Methods -----------------------------------------------------------------------------

    // 선택된 캐릭터 반환
    public static Character GetSelectedCharacter()
	{
		return selectedCharacter;
	}

    // 선택된 아이템 반환
    public static SKillItem GetSelectedItem()
    {
        return selectedSKillItem;
    }

    //선택된 캐릭터의 객체, 해당 캐릭터의 인덱스를 저장
    public static void SetSelectedCharacter(Character character, int index)
	{
		selectedCharacter = character;
		playerData.selectedCharacterIndex = index;
        

        SavePlayerData();
	}
	
	//아이템
    public static void SetSelectedItem(SKillItem sk_item, int index)
    {
        selectedSKillItem = sk_item;
        playerData.selectedItemIndex = index;

        SavePlayerData();
    }

    

    //선택한 캐릭터 인덱스 반환
    public static int GetSelectedCharacterIndex()
	{
		return playerData.selectedCharacterIndex;
	}

    public static int GetSelectedItemIndex()
    {
        return playerData.selectedItemIndex;
    }


    //보유한 코인 수 반환
    public static int GetCoins ()
	{
		return playerData.coins;
	}

	//코인 추가
	public static void AddCoins (int amount)
	{
     
        playerData.coins += amount;
		SavePlayerData ();
	}

	

	//쓸수 있는 코인
	public static bool CanSpendCoins (int amount)
	{
		return (playerData.coins >= amount);
	}

	//코인 소비
	public static void SpendCoins (int amount)
	{
		playerData.coins -= amount;
		SavePlayerData ();
	}

	//플레이어 데이터 로드
	static void LoadPlayerData ()
	{
		playerData = BinarySerializer.Load<PlayerData> ("player-data.txt");
		UnityEngine.Debug.Log ("<color=green>[PlayerData] Loaded.</color>");
	}

	//플레이어 데이터 저장
	static void SavePlayerData ()
	{
		BinarySerializer.Save (playerData, "player-data.txt");
		UnityEngine.Debug.Log ("<color=magenta>[PlayerData] Saved.</color>");
	}

	//Characters Shop Data Methods -----------------------------------------------------------------------------

	//구매한 캐릭터 추가
	public static void AddPurchasedCharacter (int characterIndex)
	{
		charactersShopData.purchasedCharactersIndexes.Add (characterIndex);
		SaveCharactersShoprData ();
	}

	//모든 구매한 캐릭터 인덱스 목록 반환
	public static List<int> GetAllPurchasedCharacter ()
	{
		return charactersShopData.purchasedCharactersIndexes;
	}

	//특정 인덱스의 구매한 캐릭터 인덱스 반환
	public static int GetPurchasedCharacter (int index)
	{
		return charactersShopData.purchasedCharactersIndexes [index];
	}

    //스피드 추가
    public static float GetcharacterSpeed()
    {
        return selectedCharacter.speed;
    }
   
    //Items Shop Data Methods -----------------------------------------------------------------------------

    //구매한 아이템 추가
    public static void AddPurchasedItem(int characterIndex)
    {
        ItemsShopData.purchasedItemsIndexes.Add(characterIndex);
        SaveCharactersShoprData();
    }

	//모든 구매한 아이템 인덱스 목록 반환

	public static List<int> GetAllPurchasedItem()
	{
		return ItemsShopData.purchasedItemsIndexes;
	}

    //특정 인덱스의 아이템 캐릭터 인덱스 반환
    public static int GetPurchasedItem(int index)
    {
        return ItemsShopData.purchasedItemsIndexes[index];
    }

	//스피드 추가
	public static float GetItemSpeed()
	{
		return selectedSKillItem.speed;
	}

	//부활추가
	public static bool GetisRespawn()
	{
		return selectedSKillItem.respawn;

    }

	

    //상점 데이터 로드    
    static void LoadCharactersShopData ()
	{
		charactersShopData = BinarySerializer.Load<CharactersShopData> ("characters-shop-data.txt");
        ItemsShopData = BinarySerializer.Load<ItemsShopData>("items-shop-data.txt");
        UnityEngine.Debug.Log ("<color=green>[CharactersShopData] Loaded.</color>");
	}


    //상점 데이터 저장
    static void SaveCharactersShoprData ()
	{
		BinarySerializer.Save (charactersShopData, "characters-shop-data.txt");
        BinarySerializer.Save(ItemsShopData, "items-shop-data.txt");
        UnityEngine.Debug.Log ("<color=magenta>[CharactersShopData] Saved.</color>");
	}
}
