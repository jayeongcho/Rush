using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillItemUI : MonoBehaviour
{
    [SerializeField] Color sk_itemNotSelctedColor; //선택안된경우 색상
    [SerializeField] Color sk_itemSelectedColor; //선택된 경우 색상

    [Space(20f)] //그냥 공간생성
    [SerializeField] Image skillImage;
    [SerializeField] TMP_Text skillNameText;
   // [SerializeField] TMP_Text skillInfoText;
    [SerializeField] TMP_Text skillSpeedText;
    // [SerializeField] Image skillSpeedFill;
    //[SerializeField] Image skillPowerFill;
    [SerializeField] TMP_Text skillPriceText;
    [SerializeField] Button skillPurchaseButton;

    [Space(20f)]
    [SerializeField] Button itemButton;
    [SerializeField] Image itemImage;
    [SerializeField] Outline itemOutline;

    //-----------------------

    //아이템 위치
    public void SetItemPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition += pos;
    }

    //캐릭터 이미지
    public void SetSkillImage(Sprite sprite)
    {
        skillImage.sprite = sprite;
    }

    //캐릭터 이름 설정
    public void SetSkillName(string name)
    {
        skillNameText.text = name;
    }
    //public void SetSkillInfo(string info)
    //{
    //    skillInfoText.text = info;
    //}

    public void SetSkillSpeed(float speed)
    {
        skillSpeedText.text = speed.ToString();
    }


    //캐릭터 가격 설정
    public void SetSkillPrice(int price)
    {
        skillPriceText.text = price.ToString();
    }

    //이미구매한 캐릭터 표시
    public void SetSkillAsPurchased()
    {
        skillPurchaseButton.gameObject.SetActive(false);

        // 버튼을 상호작용 가능한 상태로 설정합니다.

        itemButton.interactable = true;

        itemImage.color = sk_itemNotSelctedColor;


    }
    //아이템 구매시 실행될 이벤트 등록
    //events : eventname( item_index, methodToExcute)
    public void OnItemPurchase(int itemIndex, UnityAction<int> action)
    {
        skillPurchaseButton.onClick.RemoveAllListeners();
        skillPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    //아이템 선택시 실행될 이벤트 등록

    public void OnItemSelect(int itemIndex, UnityAction<int> action)
    {
        itemButton.interactable = true;
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    //아이템 선택된 상태로 설정
    public void SelectItem()
    {
        itemOutline.enabled = true; //아웃라인활성화
        itemImage.color = sk_itemSelectedColor;
        itemButton.interactable = false;
    }

    //아이템 선택안된 상태로 설정
    public void DeselectedItem()
    {
        itemOutline.enabled = false;
        itemImage.color = sk_itemNotSelctedColor;
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
        transform.DOShakePosition(1f, new Vector3(8f, 0, 0), 10, 0).SetEase(Ease.Linear);
    }
}


