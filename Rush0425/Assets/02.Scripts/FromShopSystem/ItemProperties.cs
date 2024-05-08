using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemProperties : MonoBehaviour
{
    [SerializeField] GameObject[] sk_itemPrefabs;
    [SerializeField] SKillItem sk_Item;
    [SerializeField] ItemShopDatabase itemDB;
    [SerializeField] Toony_PlayerMove player;
    

    public GameObject itemObject; //������
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
        //�������� ������ ��ų�� ������ (�׿� ������Ʈ ��Ȱ��ȭ)
        skills = GameDataManager.GetSelectedItem();
        selectItem = GameDataManager.GetSelectedItemIndex();
        Debug.Log("selectItem" + selectItem);

        sk_itemPrefabs[selectItem].SetActive(true);
        itemObject = sk_itemPrefabs[selectItem];

        for (int i = 0; i < sk_itemPrefabs.Length; i++)
        {

            if (i != selectItem)
            { //���þȵȰ� ��Ȱ��ȭ
                sk_itemPrefabs[i].SetActive(false);
            }
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false); //���� ������Ʈ ��������ϱ�

        //��������
        if (skills.doblecoin > 10)
        {
            // 5�� �Ŀ� ���� ��Ȱ��ȭ �޼��� ȣ��
            Invoke("DeactivateAllChildren", itemTime);
            // ���� Ȱ��ȭ �޼��� ȣ��
            ActivateAllChildren();
        }

        //�ӵ� 2��
       
        float itemSpeed = GameDataManager.GetItemSpeed();
        Debug.Log("itemSpeed" + itemSpeed);
        if (itemSpeed > 9)
        {
            player.moveSpeed = itemSpeed;
        }
        // 5�� �Ŀ� ĳ���ͼӵ��� ���ư��� �޼��� ȣ��
        Invoke("DeactivateSpeed", itemTime);


        
        bool isRespawn = GameDataManager.GetisRespawn();
        if (isRespawn )
        {
            getRespawnItem = true;
            Debug.Log(isRespawn);
            Debug.Log(getRespawnItem);
        }


    }
  
   

    //collectcoin ��ũ��Ʈ�� �ڽ� Ȱ��ȭ
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
        //5�ʵڿ� ��Ȱ��ȭ
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
