using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillItemUI : MonoBehaviour
{
    [SerializeField] Color sk_itemNotSelctedColor; //���þȵȰ�� ����
    [SerializeField] Color sk_itemSelectedColor; //���õ� ��� ����

    [Space(20f)] //�׳� ��������
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

    //������ ��ġ
    public void SetItemPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition += pos;
    }

    //ĳ���� �̹���
    public void SetSkillImage(Sprite sprite)
    {
        skillImage.sprite = sprite;
    }

    //ĳ���� �̸� ����
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


    //ĳ���� ���� ����
    public void SetSkillPrice(int price)
    {
        skillPriceText.text = price.ToString();
    }

    //�̹̱����� ĳ���� ǥ��
    public void SetSkillAsPurchased()
    {
        skillPurchaseButton.gameObject.SetActive(false);

        // ��ư�� ��ȣ�ۿ� ������ ���·� �����մϴ�.

        itemButton.interactable = true;

        itemImage.color = sk_itemNotSelctedColor;


    }
    //������ ���Ž� ����� �̺�Ʈ ���
    //events : eventname( item_index, methodToExcute)
    public void OnItemPurchase(int itemIndex, UnityAction<int> action)
    {
        skillPurchaseButton.onClick.RemoveAllListeners();
        skillPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    //������ ���ý� ����� �̺�Ʈ ���

    public void OnItemSelect(int itemIndex, UnityAction<int> action)
    {
        itemButton.interactable = true;
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    //������ ���õ� ���·� ����
    public void SelectItem()
    {
        itemOutline.enabled = true; //�ƿ�����Ȱ��ȭ
        itemImage.color = sk_itemSelectedColor;
        itemButton.interactable = false;
    }

    //������ ���þȵ� ���·� ����
    public void DeselectedItem()
    {
        itemOutline.enabled = false;
        itemImage.color = sk_itemNotSelctedColor;
        itemButton.interactable = true;
    }

    //Shake item (ȿ��)
    public void AnimateShakeItem()
    {
        //End all animation first
        //�ִϸ��̼� ������� , ������ġ�� ���·� �̵�
        transform.DOComplete();

        //DoshakePosition(�ִϸ��̼����ӽð�, ����(����), Ƚ��, ���۽ð��� ������).
        //setEase�ִϸ��̼��� ��¡(�ε巯�� ����) 
        //(Ease.Linear) : ������¡
        transform.DOShakePosition(1f, new Vector3(8f, 0, 0), 10, 0).SetEase(Ease.Linear);
    }
}


