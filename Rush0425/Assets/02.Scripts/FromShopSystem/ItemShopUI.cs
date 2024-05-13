using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemShopUI : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = .5f; //아이템간 간격
    float itemHeight;// 높이

    [Header("UI elemetns")]
    [SerializeField] Image selectedItemIcon; //상점메뉴상단 캐릭터아이콘
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

    //아이템 선택
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

    //상점ItemUI생성
    private void GenerateShopItemUI()
    {

        //중복되는 UI생성방지

        //Loop thorow save purchased items and make them as purchased in th Database array
        //반복문을 통해 이미 구매된 아이템을 가져와 데이터베이스배열에서 해당 아이템을 구매된 상태로 표시
        for (int i = 0; i < GameDataManager.GetAllPurchasedItem().Count; i++)
        {
            //각 구매된 아이템의 인덱스를 변수에 저장 (데이터관리자에서 구매된 아이템을 가져올때 사용)
            int purchasedItemIndex = GameDataManager.GetPurchasedItem(i);

            //데이터베이스 배열에서 해당 아이템을 구매된 상태로 표시
            skillitemDB.PurchaseItem(purchasedItemIndex);
        }

        // Delete itemTemplate after calculating items Height: 아이템 높이계산후 삭제
        itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemsContainer.GetChild(0).gameObject); //첫번째자식삭제(아이템템플릿)
        ShopItemsContainer.DetachChildren(); //분리

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
                //Item is purchased 스킬아이템 구매시
                uiItem.SetSkillAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelected);
            }
            else
            {
                //Item is not purchased yet 아직 구매안된 스킬아이템
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
        //Selected item inthe UI UI에서 선택된 아이템
        SelectItemUI(index);

        // set selectedCharacter Image at the top of shop menu 맨위 캐릭터 선택
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

        //캐릭터정보 데이터베이스에서 가져오기 (코인필요)
        Character character = characterDB.GetCharacter(index);



        //코인 있을때
        if (GameDataManager.CanSpendCoins(character.price))
        {
            //Proceed with purchase operation (코인소비)
            GameDataManager.SpendCoins(character.price);
            //Play purchase Fx
            purchaseFx.Play();
            purchaseSound.Play();

            //Updata Coins UI text
            GameSharedUI.Instance.UpdateCoinsUIText();
            //데이터베이스에 구매표시
            skillitemDB.PurchaseItem(index);

            //ui표시
            sk_uiItem.SetSkillAsPurchased();
            sk_uiItem.OnItemPurchase(index, OnItemSelected);
            //Add purchased item to Shop Data (구매한 아이템을데이터관리자에 추가)
            GameDataManager.AddPurchasedItem(index);

            GenerateShopItemUI();
        }
        else //코인부족시 실행
        {
            //No enough coins.
            AnimateNoMoreCoinsText();

            //item shake
            sk_uiItem.AnimateShakeItem();
        }
    }

    //코인부족시 나오는 효과
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
        //Ease.OutBack :약간뒤로 이동하여 마무리되는 효과, 스크롤을 더 부드럽게 만들어주는데 사용됨
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
    //Shop 열기
    void OpenShop()
    {
        shopUI.SetActive(true);
    }

    //Shop닫기
    void CloseShop()
    {
        shopUI.SetActive(false);
    }
}
