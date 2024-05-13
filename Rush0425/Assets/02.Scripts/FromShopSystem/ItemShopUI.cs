using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemShopUI : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = .5f; //�����۰� ����
    float itemHeight;// ����

    [Header("UI elemetns")]
    [SerializeField] Image selectedItemIcon; //�����޴���� ĳ���;�����
    [SerializeField] Transform ShopMenu;
    [SerializeField] Transform ShopItemsContainer;
    [SerializeField] GameObject itemPrefab;

    [Space(20)]
    [SerializeField] ItemShopDatabase skillitemDB;
    [SerializeField] CharacterShopDatabase characterDB;

    [Space(20)]
    [Header("ShopEvents")]
    [SerializeField] GameObject shopUI;
    [SerializeField] Button openShopButton;
    [SerializeField] Button closeShopButton;
    [SerializeField] Button scrollUpButton;

    [Space(20)]
    [Header("Scroll View")]
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject topScrollFade;
    [SerializeField] GameObject bottomScrollFade;


    [Space(20)]
    [Header("Purchase Fx & Error messages")]
    [SerializeField] ParticleSystem purchaseFx;
    [SerializeField] Transform purchaseFxPos;
    [SerializeField] TMP_Text noEnoughCoinsText;
    [SerializeField] AudioSource purchaseSound;

    int newSelectedItemIndex = 0;
    int previousSelectedItemIndex = 0;


    void Start()
    {
        AddShopEvents();
        GenerateShopItemUI();

        SetSelectedItem();

        SelectItemUI(GameDataManager.GetSelectedItemIndex());

        //Auto scroll to selected character in the shop
        AutoScrollShopList(GameDataManager.GetSelectedCharacterIndex());
    }

    //������ ����
    void SetSelectedItem()
    {
        //Get Saved Index
        int index = GameDataManager.GetSelectedItemIndex();

        //Set selected character
        GameDataManager.SetSelectedItem(skillitemDB.GetItem(index), index);
    }


    void AutoScrollShopList(int itemIndex)
    {
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(1f - (itemIndex / (float)(skillitemDB.ItemsCount - 1)));
    }

    //����ItemUI����
    private void GenerateShopItemUI()
    {

        //�ߺ��Ǵ� UI��������

        //Loop thorow save purchased items and make them as purchased in th Database array
        //�ݺ����� ���� �̹� ���ŵ� �������� ������ �����ͺ��̽��迭���� �ش� �������� ���ŵ� ���·� ǥ��
        for (int i = 0; i < GameDataManager.GetAllPurchasedItem().Count; i++)
        {
            //�� ���ŵ� �������� �ε����� ������ ���� (�����Ͱ����ڿ��� ���ŵ� �������� �����ö� ���)
            int purchasedItemIndex = GameDataManager.GetPurchasedItem(i);

            //�����ͺ��̽� �迭���� �ش� �������� ���ŵ� ���·� ǥ��
            skillitemDB.PurchaseItem(purchasedItemIndex);
        }

        // Delete itemTemplate after calculating items Height: ������ ���̰���� ����
        itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemsContainer.GetChild(0).gameObject); //ù��°�ڽĻ���(���������ø�)
        ShopItemsContainer.DetachChildren(); //�и�

        //Generate Items
        for (int i = 0; i < skillitemDB.ItemsCount; i++)
        {
            SKillItem skillitem = skillitemDB.GetItem(i);
            SkillItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<SkillItemUI>();

            //Move item to its position
            uiItem.SetItemPosition(Vector2.down * i * (itemHeight + itemSpacing));

            //Set Item name in Hierachy(NOt required)
            uiItem.gameObject.name = "Item" + i + "-" + skillitem.name;

            //Add information to the UI( one item)
            uiItem.SetSkillName(skillitem.name);
            uiItem.SetSkillInfo(skillitem.info);
            uiItem.SetSkillImage(skillitem.image);
            uiItem.SetSkillSpeed(skillitem.speed);

            // uiItem.SetSkillPower(skillitem.power);
            uiItem.SetSkillPrice(skillitem.price);



            if (skillitem.isPurchased)
            {
                //Item is purchased ��ų������ ���Ž�
                uiItem.SetSkillAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelected);
            }
            else
            {
                //Item is not purchased yet ���� ���žȵ� ��ų������
                uiItem.SetSkillPrice(skillitem.price);
                uiItem.OnItemPurchase(i, OnItemPurchased);

            }

            //Resize Item Container
            ShopItemsContainer.GetComponent<RectTransform>().sizeDelta =
                Vector2.up * ((itemHeight + itemSpacing) * skillitemDB.ItemsCount + itemSpacing);

        }
    }

    void OnItemSelected(int index)
    {
        //Selected item inthe UI UI���� ���õ� ������
        SelectItemUI(index);

        // set selectedCharacter Image at the top of shop menu ���� ĳ���� ����
        selectedItemIcon.sprite = skillitemDB.GetItem(index).image;

        //SaveData
        GameDataManager.SetSelectedItem(skillitemDB.GetItem(index), index);

        //ChangePlayerSkin
        // ChangePlayerSkin();
    }
    void SelectItemUI(int itemIndex)
    {
        previousSelectedItemIndex = newSelectedItemIndex;
        newSelectedItemIndex = itemIndex;

        SkillItemUI previousUiItem = SK_GetItemUI(previousSelectedItemIndex);
        SkillItemUI newUiItem = SK_GetItemUI(newSelectedItemIndex);

        previousUiItem.DeselectedItem();
        newUiItem.SelectItem();

    }

    SkillItemUI SK_GetItemUI(int itemIndex)
    {
        return ShopItemsContainer.GetChild(itemIndex).GetComponent<SkillItemUI>();
    }
    CharacterItemUI GetItemUI(int itemIndex)
    {
        return ShopItemsContainer.GetChild(itemIndex).GetComponent<CharacterItemUI>();
    }
    void OnItemPurchased(int index)
    {
        SKillItem skillitem = skillitemDB.GetItem(index);
        SkillItemUI sk_uiItem = SK_GetItemUI(index);

        //ĳ�������� �����ͺ��̽����� �������� (�����ʿ�)
        Character character = characterDB.GetCharacter(index);



        //���� ������
        if (GameDataManager.CanSpendCoins(character.price))
        {
            //Proceed with purchase operation (���μҺ�)
            GameDataManager.SpendCoins(character.price);
            //Play purchase Fx
            purchaseFx.Play();
            purchaseSound.Play();

            //Updata Coins UI text
            GameSharedUI.Instance.UpdateCoinsUIText();
            //�����ͺ��̽��� ����ǥ��
            skillitemDB.PurchaseItem(index);

            //uiǥ��
            sk_uiItem.SetSkillAsPurchased();
            sk_uiItem.OnItemPurchase(index, OnItemSelected);
            //Add purchased item to Shop Data (������ �������������Ͱ����ڿ� �߰�)
            GameDataManager.AddPurchasedItem(index);

            GenerateShopItemUI();
        }
        else //���κ����� ����
        {
            //No enough coins.
            AnimateNoMoreCoinsText();

            //item shake
            sk_uiItem.AnimateShakeItem();
        }
    }

    //���κ����� ������ ȿ��
    void AnimateNoMoreCoinsText()
    {
        //Complete animations (if it'w naming)
        noEnoughCoinsText.transform.DOComplete();
        noEnoughCoinsText.DOComplete();

        //Shaking Text
        noEnoughCoinsText.transform.DOShakePosition(3f, new Vector3(5f, 0f, 0f), 10, 0);
        noEnoughCoinsText.DOFade(1f, 3f).From(0f).OnComplete(() =>
        {
            noEnoughCoinsText.DOFade(0f, 1f);
        });

    }
    void AddShopEvents()
    {
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);

        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);

        scrollRect.onValueChanged.RemoveAllListeners();
        scrollRect.onValueChanged.AddListener(OnShopListScroll);

        scrollUpButton.onClick.RemoveAllListeners();
        scrollUpButton.onClick.AddListener(OnScrollUpClicked);
    }

    void OnScrollUpClicked()
    {
        //Ease.OutBack :�ణ�ڷ� �̵��Ͽ� �������Ǵ� ȿ��, ��ũ���� �� �ε巴�� ������ִµ� ����
        scrollRect.DOVerticalNormalizedPos(1f, .5f).SetEase(Ease.OutBack);
    }

    void OnShopListScroll(Vector2 value)
    {
        float scrollY = value.y;
        //Topfade
        if (scrollY < .1f)
            topScrollFade.SetActive(true);
        else
            topScrollFade.SetActive(false);

        //Bottomfade
        if (scrollY > 0f)
            bottomScrollFade.SetActive(true);
        else
            bottomScrollFade.SetActive(false);

        //Scroll Up button
        if (scrollY < .7f)
        {
            scrollUpButton.gameObject.SetActive(true);
        }
        else
        {
            scrollUpButton.gameObject.SetActive(false);
        }

    }
    //Shop ����
    void OpenShop()
    {
        shopUI.SetActive(true);
    }

    //Shop�ݱ�
    void CloseShop()
    {
        shopUI.SetActive(false);
    }
}
