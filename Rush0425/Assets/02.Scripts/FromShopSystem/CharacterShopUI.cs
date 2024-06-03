using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CharacterShopUI : MonoBehaviour
{

    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = .5f; //아이템간 간격
    float itemHeight;// 높이

    [Header("UI elemetns")]
    [SerializeField] Image selectedCharacterIcon; //상점메뉴상단 캐릭터아이콘
    [SerializeField] Transform ShopMenu;//상점메뉴
    [SerializeField] Transform ShopItemsContainer;// 상점 아이템을 담는 컨테이너
    [SerializeField] GameObject itemPrefab;// 아이템 프리팹
    [Space(20)]
    [SerializeField] CharacterShopDatabase characterDB; //캐릭터 데이터베이스

    [Space(20)]
    [Header("ShopEvents")]
    [SerializeField] GameObject shopUI;//상점UI
    [SerializeField] Button openShopButton;//상점 열기 버튼
    [SerializeField] Button closeShopButton;//상점 닫기 버튼
    [SerializeField] Button scrollUpButton;// 스크롤 위로 버튼


    //  [Space(20)]
    [Header("Main Menu")]
    [SerializeField] GameObject[] characterPrefabs; //캐릭터 프리팹 배열
    // [SerializeField] Image mainMenuCharacterImage;
    //  [SerializeField] TMP_Text mainMenuCharacterName;

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
        purchaseFx.transform.position = purchaseFxPos.position;

        AddShopEvents();
        GenerateShopItemUI();

        //플레이어 데이터 관리자에서 선택된 캐릭터 설정
        //Set Selected character in the playerDataManager
        SetSelectedCharacter();

        //선택된 ui아이템 선택
        //Select UI item
        SelectItemUI(GameDataManager.GetSelectedCharacterIndex());

        //플레이어 스킨 변경
        //set player skin
        ChangePlayerSkin();

        //상점에서 선택된 캐릭터로 자동 스크롤
        //Auto scroll to selected character in the shop
        AutoScrollShopList(GameDataManager.GetSelectedCharacterIndex());

    }


    //상점 리스트 자동 스크롤
    void AutoScrollShopList(int itemIndex)
    {
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(1f - (itemIndex / (float)(characterDB.CharactersCount - 1)));
    }

    //캐릭터 선택
    void SetSelectedCharacter()
    {
        //Get Saved Index
        int index = GameDataManager.GetSelectedCharacterIndex();

        //Set selected character
        GameDataManager.SetSelectedCharacter(characterDB.GetCharacter(index), index);
    }


    //상점ItemUI생성
    void GenerateShopItemUI()
    {
        //중복되는 UI생성방지
        //Loop thorow save purchased items and make them as purchased in th Database array
        //반복문을 통해 이미 구매된 아이템을 가져와 데이터베이스배열에서 해당 아이템을 구매된 상태로 표시
        for (int i = 0; i < GameDataManager.GetAllPurchasedCharacter().Count; i++)
        {
            //각 구매된 아이템의 인덱스를 변수에 저장 (데이터관리자에서 구매된 아이템을 가져올때 사용)
            int purchasedCharacterIndex = GameDataManager.GetPurchasedCharacter(i);
            Debug.Log("purchasedCharacerIdex" + purchasedCharacterIndex);
            //데이터베이스 배열에서 해당 아이템을 구매된 상태로 표시
            characterDB.PurchaseCharacter(purchasedCharacterIndex);
        }

        // Delete itemTemplate after calculating items Height: 아이템 높이계산후 삭제
        itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemsContainer.GetChild(0).gameObject); //첫번째자식삭제(아이템템플릿)
        ShopItemsContainer.DetachChildren(); //분리

        //Generate Items
        for (int i = 0; i < characterDB.CharactersCount; i++)
        {
            Character character = characterDB.GetCharacter(i);
            CharacterItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<CharacterItemUI>();

            //Move item to its position
            uiItem.SetItemPosition(Vector2.down * i * (itemHeight + itemSpacing));

            //Set Item name in Hierachy(NOt required)
            uiItem.gameObject.name = "Item" + i + "-" + character.name;

            //Add information to the UI( one item)
            uiItem.SetCharacterName(character.name);
            uiItem.SetCharacterImage(character.image);
            uiItem.SetCharacterSpeed(character.speed);
            // uiItem.SetCharacterPower(character.power);
            uiItem.SetCharacterPrice(character.price);



            if (character.isPurchased)
            {
                //Character is purchased캐릭터 구매시
                uiItem.SetCharacterAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelected);
            }
            else
            {
                //character is not purchased yet 아직 구매안된 캐릭터
                uiItem.SetCharacterPrice(character.price);
                uiItem.OnItemPurchase(i, OnItemPurchased);

            }

            //Resize Item Container
            ShopItemsContainer.GetComponent<RectTransform>().sizeDelta =
                Vector2.up * ((itemHeight + itemSpacing) * characterDB.CharactersCount + itemSpacing);

        }
    }
    //void ChangePlayerSkin()
    //{
    //    Character character = GameDataManager.GetSelectedCharacter();
    //    if (character.image != null)
    //    {
    //        mainMenuCharacterImage.sprite = character.image;
    //        mainMenuCharacterName.text = character.name;

    //        // set selectedCharacter Image at the top of shop menu 맨위 캐릭터 선택
    //        selectedCharacterIcon.sprite = GameDataManager.GetSelectedCharacter().image;
    //    }
    //}


    //캐릭터 변경
    void ChangePlayerSkin()
    {
        Character character = GameDataManager.GetSelectedCharacter();

        int selectedSkin = GameDataManager.GetSelectedCharacterIndex();

        //게임오브젝트를 활성화시키고, 선택안된건 비활성화
        characterPrefabs[selectedSkin].SetActive(true);
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            if (i != selectedSkin)
            { characterPrefabs[i].SetActive(false); }

        }

        selectedCharacterIcon.sprite = GameDataManager.GetSelectedCharacter().image;
    }

    //아이템 선택 이벤트 처리
    void OnItemSelected(int index)
    {
        //Selected item inthe UI UI에서 선택된 아이템
        SelectItemUI(index);

        // set selectedCharacter Image at the top of shop menu 맨위 캐릭터 선택
         selectedCharacterIcon.sprite = characterDB.GetCharacter(index).image;

        //SaveData
        GameDataManager.SetSelectedCharacter(characterDB.GetCharacter(index), index);

        //ChangePlayerSkin
        ChangePlayerSkin();
    }

    //인덱스를 기반으로 아이템 선택하는 ui동작
    void SelectItemUI(int itemIndex)
    {
        previousSelectedItemIndex = newSelectedItemIndex; //이전에 선택된 아이템 인덱스 저장
        newSelectedItemIndex = itemIndex; //새로 선택된 아이템 인덱스

        CharacterItemUI previousUiItem = GetItemUI(previousSelectedItemIndex); //이전 선택 아이템 ui
        CharacterItemUI newUiItem = GetItemUI(newSelectedItemIndex);//새로선택된 아이템ui

        previousUiItem.DeselectedItem(); //이전 선택 ui를 선택 해제
        newUiItem.SelectItem();// 새로 선택된 아이템ui를 선태 처리

    }

    //아이템ui가져오기
    CharacterItemUI GetItemUI(int itemIndex)
    {
        return ShopItemsContainer.GetChild(itemIndex).GetComponent<CharacterItemUI>();
    }

    //  아이템 구매
    void OnItemPurchased(int index)
    {
        //아이템 정보 데이터베이스에서 가져오기
        Character character = characterDB.GetCharacter(index);
        //ui요소 가져오기
        CharacterItemUI uiItem = GetItemUI(index);
        
        //코인있을때
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
            characterDB.PurchaseCharacter(index);

            //ui표시
            uiItem.SetCharacterAsPurchased();
            uiItem.OnItemPurchase(index, OnItemSelected);

            //Add purchased item to Shop Data (구매한 아이템을데이터관리자에 추가)
            GameDataManager.AddPurchasedCharacter(index);

            GenerateShopItemUI();


        }
        else //코인부족시 실행
        {
            //No enough coins.
            AnimateNoMoreCoinsText();

            //item shake
            uiItem.AnimateShakeItem();
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

    //상점 이벤트 추가
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

    //스크롤 위로 버튼 클릭시
    void OnScrollUpClicked()
    {
        //Ease.OutBack :약간뒤로 이동하여 마무리되는 효과, 스크롤을 더 부드럽게 만들어주는데 사용됨
        scrollRect.DOVerticalNormalizedPos(1f, .5f).SetEase(Ease.OutBack);
    }

    // 상점 리스트 스크롤 시
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
