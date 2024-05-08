using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemProperties : MonoBehaviour
{
    [SerializeField] GameObject[] sk_itemPrefabs;
    [SerializeField] SKillItem sk_Item;
    [SerializeField] ItemShopDatabase itemDB;
    [SerializeField] Toony_PlayerMove player;
    

    public GameObject itemObject; //아이템
    public CollectCoin[] coin;
    public ObstacleCollision[] obstacle; 

    public float itemTime = 5f;
    SKillItem skills;
    int selectItem;
    public static bool getRespawnItem = false;
    

    // Start is called before the first frame update
    void Start()
    {
        ChangeSkillProperties();

    }
    void ChangeSkillProperties()
    {
        //상점에서 선택한 스킬만 보여짐 (그외 오브젝트 비활성화)
        skills = GameDataManager.GetSelectedItem();
        selectItem = GameDataManager.GetSelectedItemIndex();
        Debug.Log("selectItem" + selectItem);

        sk_itemPrefabs[selectItem].SetActive(true);
        itemObject = sk_itemPrefabs[selectItem];

        for (int i = 0; i < sk_itemPrefabs.Length; i++)
        {

            if (i != selectItem)
            { //선택안된건 비활성화
                sk_itemPrefabs[i].SetActive(false);
            }
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false); //먹은 오브젝트 사라지게하기

        //더블코인
        if (skills.doblecoin > 10)
        {
            // 5초 후에 코인 비활성화 메서드 호출
            Invoke("DeactivateAllChildren", itemTime);
            // 코인 활성화 메서드 호출
            ActivateAllChildren();
        }

        //속도 2배
       
        float itemSpeed = GameDataManager.GetItemSpeed();
        Debug.Log("itemSpeed" + itemSpeed);
        if (itemSpeed > 9)
        {
            player.moveSpeed = itemSpeed;
        }
        // 5초 후에 캐릭터속도로 돌아가는 메서드 호출
        Invoke("DeactivateSpeed", itemTime);


        
        bool isRespawn = GameDataManager.GetisRespawn();
        if (isRespawn )
        {
            getRespawnItem = true;
            Debug.Log(isRespawn);
            Debug.Log(getRespawnItem);
        }


    }
  
   

    //collectcoin 스크립트의 자식 활성화
    private void ActivateAllChildren()
    {
        coin = FindObjectsOfType<CollectCoin>();
        foreach (CollectCoin coins in coin)
        {
            coins.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void DeactivateAllChildren()
    {
        //5초뒤에 비활성화
        coin = FindObjectsOfType<CollectCoin>();
        foreach (CollectCoin coins in coin)
        {
            coins.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void DeactivateSpeed()
    {
        player.moveSpeed = GameDataManager.GetcharacterSpeed();
    }

}
