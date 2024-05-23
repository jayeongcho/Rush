using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
public class CharacterItemUI : MonoBehaviour
{
    [SerializeField] Color char_itemNotSelctedColor; //���þȵȰ��  ����
    [SerializeField] Color char_itemSelectedColor; //���õ� ��� ����

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

    //������ ��ġ ����
    public void SetItemPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition += pos;
    }

    //ĳ���� �̹��� ����
    public void SetCharacterImage(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }

    //ĳ���� �̸� ����
    public void SetCharacterName(string name)
    {
        characterNameText.text = name;
    }

    //ĳ���� �ӵ� ����
    public void SetCharacterSpeed(float speed)
    {
        characterSpeedFill.fillAmount = speed/10;
    }

    //ĳ���� �Ŀ� ����
    //public void SetCharacterPower(float power)
    //{
    //    characterPowerFill.fillAmount = power / 100;
    //}

    //ĳ���� ���� ����
    public void SetCharacterPrice(int price)
    {
        characterPriceText.text = price.ToString();
    }

    //�̹̱����� ĳ���� ǥ�� : ���Ź�ư ��Ȱ��ȭ, ������ ��ư Ȱ��ȭ, ���� ����
    public void SetCharacterAsPurchased()
    {
        characterPurchaseButton.gameObject.SetActive(false);
      
        // ��ư�� ��ȣ�ۿ� ������ ���·� �����մϴ�.
        
        itemButton.interactable = true;
        
        itemImage.color = char_itemNotSelctedColor;


    }
    //������ ���Ź�ư Ŭ���� ����� �̺�Ʈ ���
    //action : ��ư Ŭ���� ������ �ż���
    //events : eventname( item_index, methodToExcute)
    public void OnItemPurchase(int itemIndex, UnityAction<int> action)
    {
        characterPurchaseButton.onClick.RemoveAllListeners();
        characterPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    //������ Ŭ���� ����� �̺�Ʈ ���   
    public void OnItemSelect(int itemIndex, UnityAction<int> action)
    {
        Debug.Log("onItemSelect");
        itemButton.interactable = true;
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    //������ ���õ� ���·� ����
   public void SelectItem()
    {
        itemOutline.enabled = true; //�ƿ�����Ȱ��ȭ
        itemImage.color = char_itemSelectedColor;
        
        itemButton.interactable = false;
    }
   
    //������ ���þȵ� ���·� ����
    public void DeselectedItem()
    {
        itemOutline.enabled = false;
        itemImage.color = char_itemNotSelctedColor; 
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
       transform.DOShakePosition(1f, new Vector3(8f, 0, 0) ,10, 0).SetEase(Ease.Linear);
    }
}
