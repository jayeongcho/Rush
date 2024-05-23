using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
public class CharacterItemUI : MonoBehaviour
{
    [SerializeField] Color char_itemNotSelctedColor; //선택안된경우  색상
    [SerializeField] Color char_itemSelectedColor; //선택된 경우 색상

    [Space(20f)] 
    [SerializeField] Image characterImage; 
    [SerializeField] TMP_Text characterNameText;
    [SerializeField] Image characterSpeedFill;
    //[SerializeField] Image characterPowerFill;
    [SerializeField] TMP_Text characterPriceText;
    [SerializeField] Button characterPurchaseButton;

    [Space(20f)]
    [SerializeField] Button itemButton;
    [SerializeField] Image itemImage;
    [SerializeField] Outline itemOutline;

    //-----------------------

    //아이템 위치 설정
    public void SetItemPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition += pos;
    }

    //캐릭터 이미지 설정
    public void SetCharacterImage(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }

    //캐릭터 이름 설정
    public void SetCharacterName(string name)
    {
        characterNameText.text = name;
    }

    //캐릭터 속도 설정
    public void SetCharacterSpeed(float speed)
    {
        characterSpeedFill.fillAmount = speed/10;
    }

    //캐릭터 파워 설정
    //public void SetCharacterPower(float power)
    //{
    //    characterPowerFill.fillAmount = power / 100;
    //}

    //캐릭터 가격 설정
    public void SetCharacterPrice(int price)
    {
        characterPriceText.text = price.ToString();
    }

    //이미구매한 캐릭터 표시 : 구매버튼 비활성화, 아이템 버튼 활성화, 색상 변경
    public void SetCharacterAsPurchased()
    {
        characterPurchaseButton.gameObject.SetActive(false);
      
        // 버튼을 상호작용 가능한 상태로 설정합니다.
        
        itemButton.interactable = true;
        
        itemImage.color = char_itemNotSelctedColor;


    }
    //아이템 구매버튼 클릭시 실행될 이벤트 등록
    //action : 버튼 클릭시 실행할 매서드
    //events : eventname( item_index, methodToExcute)
    public void OnItemPurchase(int itemIndex, UnityAction<int> action)
    {
        characterPurchaseButton.onClick.RemoveAllListeners();
        characterPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    //아이템 클릭시 실행될 이벤트 등록   
    public void OnItemSelect(int itemIndex, UnityAction<int> action)
    {
        Debug.Log("onItemSelect");
        itemButton.interactable = true;
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    //아이템 선택된 상태로 설정
   public void SelectItem()
    {
        itemOutline.enabled = true; //아웃라인활성화
        itemImage.color = char_itemSelectedColor;
        
        itemButton.interactable = false;
    }
   
    //아이템 선택안된 상태로 설정
    public void DeselectedItem()
    {
        itemOutline.enabled = false;
        itemImage.color = char_itemNotSelctedColor; 
        itemButton.interactable = true;
    }

    //Shake item (효과)
    public void AnimateShakeItem()
    {
        //End all animation first
        //애니메이션 즉시종료 , 최종위치나 상태로 이동
        transform.DOComplete();

        //DoshakePosition(애니메이션지속시간, 강도(방향), 횟수, 시작시간의 오프셋).
        //setEase애니메이션의 이징(부드러운 정도) 
        //(Ease.Linear) : 선형이징
       transform.DOShakePosition(1f, new Vector3(8f, 0, 0) ,10, 0).SetEase(Ease.Linear);
    }
}
