using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CharacterShopUI : MonoBehaviour
{

    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = .5f; //�����۰� ����
    float itemHeight;// ����

    [Header("UI elemetns")]
    [SerializeField] Image selectedCharacterIcon; //�����޴���� ĳ���;�����
    [SerializeField] Transform ShopMenu;
    [SerializeField] Transform ShopItemsContainer;
    [SerializeField] GameObject itemPrefab;
    [Space(20)]
    [SerializeField] CharacterShopDatabase characterDB;

    [Space(20)]
    [Header("ShopEvents")]
    [SerializeField] GameObject shopUI;
    [SerializeField] Button openShopButton;
    [SerializeField] Button closeShopButton;
    [SerializeField] Button scrollUpButton;


    //  [Space(20)]
    [Header("Main Menu")]
    [SerializeField] GameObject[] characterPrefabs;
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

    int newSelectedItemIndex = 0;
    int previousSelectedItemIndex = 0;


    void Start()
    {
        purchaseFx.transform.position = purchaseFxPos.position;

        AddShopEvents();
        GenerateShopItemUI();

        //Set Selected character in the playerDataManager
        SetSelectedCharacter();

        //Select UI item
        SelectItemUI(GameDataManager.GetSelectedCharacterIndex());

        //set player skin
        ChangePlayerSkin();

        //Auto scroll to selected character in the shop
        AutoScrollShopList(GameDataManager.GetSelectedCharacterIndex());

    }
    void AutoScrollShopList(int itemIndex)
    {
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(1f - (itemIndex / (float)(characterDB.CharactersCount - 1)));
    }

    //ĳ���� ����
    void SetSelectedCharacter()
    {
        //Get Saved Index
        int index = GameDataManager.GetSelectedCharacterIndex();

        //Set selected character
        GameDataManager.SetSelectedCharacter(characterDB.GetCharacter(index), index);
    }

    //����ItemUI����
    private void GenerateShopItemUI()
    {

        //�ߺ��Ǵ� UI��������

        //Loop thorow save purchased items and make them as purchased in th Database array
        //�ݺ����� ���� �̹� ���ŵ� �������� ������ �����ͺ��̽��迭���� �ش� �������� ���ŵ� ���·� ǥ��
        for (int i = 0; i < GameDataManager.GetAllPurchasedCharacter().Count; i++)
        {
            //�� ���ŵ� �������� �ε����� ������ ���� (�����Ͱ����ڿ��� ���ŵ� �������� �����ö� ���)
            int purchasedCharacterIndex = GameDataManager.GetPurchasedCharacter(i);

            //�����ͺ��̽� �迭���� �ش� �������� ���ŵ� ���·� ǥ��
            characterDB.PurchaseCharacter(purchasedCharacterIndex);
        }

        // Delete itemTemplate after calculating items Height: ������ ���̰���� ����
        itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemsContainer.GetChild(0).gameObject); //ù��°�ڽĻ���(���������ø�)
        ShopItemsContainer.DetachChildren(); //�и�

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
            uiItem.SetCharacterPower(character.power);
            uiItem.SetCharacterPrice(character.price);



            if (character.isPurchased)
            {
                //Character is purchasedĳ���� ���Ž�
                uiItem.SetCharacterAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelected);
            }
            else
            {
                //character is not purchased yet ���� ���žȵ� ĳ����
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

    //        // set selectedCharacter Image at the top of shop menu ���� ĳ���� ����
    //        selectedCharacterIcon.sprite = GameDataManager.GetSelectedCharacter().image;
    //    }
    //}


    
    void ChangePlayerSkin()
    {
        //����ĳ���� ����

        Character character = GameDataManager.GetSelectedCharacter();
        
        int selectedSkin = GameDataManager.GetSelectedCharacterIndex();
        
        //���ӿ�����Ʈ�� Ȱ��ȭ��Ű��, ���þȵȰ� ��Ȱ��ȭ
        characterPrefabs[selectedSkin].SetActive(true);
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            if (i != selectedSkin)
            { characterPrefabs[i].SetActive(false); }

        }

        selectedCharacterIcon.sprite = GameDataManager.GetSelectedCharacter().image;
    }

    void OnItemSelected(int index)
    {
        //Selected item inthe UI UI���� ���õ� ������
        SelectItemUI(index);

        // set selectedCharacter Image at the top of shop menu ���� ĳ���� ����
        selectedCharacterIcon.sprite = characterDB.GetCharacter(index).image;

        //SaveData
        GameDataManager.SetSelectedCharacter(characterDB.GetCharacter(index), index);

        //ChangePlayerSkin
        ChangePlayerSkin();
    }

    //�ε����� ������� ������ �����ϴ� ui����
    void SelectItemUI(int itemIndex)
    {
        previousSelectedItemIndex = newSelectedItemIndex; //������ ���õ� ������ �ε��� ����
        newSelectedItemIndex = itemIndex; //���� ���õ� ������ �ε���

        CharacterItemUI previousUiItem = GetItemUI(previousSelectedItemIndex); //���� ���� ������ ui
        CharacterItemUI newUiItem = GetItemUI(newSelectedItemIndex);//���μ��õ� ������ui

        previousUiItem.DeselectedItem(); //���� ���� ui�� ���� ����
        newUiItem.SelectItem();// ���� ���õ� ������ui�� ���� ó��

    }
    CharacterItemUI GetItemUI(int itemIndex)
    {
        return ShopItemsContainer.GetChild(itemIndex).GetComponent<CharacterItemUI>();
    }

    //  �����۱���
    void OnItemPurchased(int index)
    {
        //������ ���� �����ͺ��̽����� ��������
        Character character = characterDB.GetCharacter(index);
        //ui��� ��������
        CharacterItemUI uiItem = GetItemUI(index);

        //����������
        if (GameDataManager.CanSpendCoins(character.price))
        {
            //Proceed with purchase operation (���μҺ�)
            GameDataManager.SpendCoins(character.price);
            //Play purchase Fx
            purchaseFx.Play();

            //Updata Coins UI text
            GameSharedUI.Instance.UpdateCoinsUIText();

            //�����ͺ��̽��� ����ǥ��
            characterDB.PurchaseCharacter(index);

            //uiǥ��
            uiItem.SetCharacterAsPurchased();
            uiItem.OnItemPurchase(index, OnItemSelected);

            //Add purchased item to Shop Data (������ �������������Ͱ����ڿ� �߰�)
            GameDataManager.AddPurchasedCharacter(index);
            Debug.Log("addpurchasedCharacter");

        }
        else //���κ����� ����
        {
            //No enough coins.
            AnimateNoMoreCoinsText();

            //item shake
            uiItem.AnimateShakeItem();
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
