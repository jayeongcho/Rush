using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelDistance : MonoBehaviour
{
    //�Ÿ�UI
   // public GameObject disDisplay;
   // public GameObject disEndDisplay;
    public int disRun;
    public bool addingDis = false;
    public float disDelay = 0.35f; //1m�� 0.35�ʸ��� ����
    public float divNum ;
    // ��UI
    public Image [] starImages; //���̹���
    public Sprite yellowStarSprite; //�����
    public Sprite greyStarSprite;//ȸ����
    public int starsCollected = 0;
    public int starsToCollect = 3;

    //���ӳ����� ���� ��
    public Image[] EndstarImages; //���̹��� 

    //����
    public int level;

    //������
    public GameObject thePlayer;
    private Toony_PlayerMove playerMoveScript;

    //�����̴�
    public Slider slider;
    float barMaxValue =100f;
    public float disRunUI;

    //����
    public float duration = 3f; // ���� ���� �ð�
    public float strength = 0.5f; // ���� ����
    public int vibrato = 10; // ���� Ƚ��

    private void Start()
    {
        playerMoveScript = thePlayer.GetComponent<Toony_PlayerMove>();
        slider.maxValue = barMaxValue;
    }
    void Update()
    {
        if (addingDis == false  && Toony_PlayerMove.canMove)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
            
        }
        
    }

    //m���� �Ÿ��߰�
    IEnumerator AddingDis()
    {
        disRun += 1;
       // disDisplay.GetComponent<Text>().text = "" + disRun; //���� �� �Ÿ� �ؽ�Ʈ 
        //disEndDisplay.GetComponent<Text>().text = "" + disRun; //���� ������ ������ �ؽ�Ʈ

        // �� ȹ�� üũ
        if (disRun % divNum == 0)
        {
            starsCollected++;

            
            PlayerPrefs.SetInt("Lv" + level, starsCollected);
           
            UpdateStarUI();
            

        }
        if(slider.value != barMaxValue)
        {
            disRunUI += 1;
            slider.value = disRunUI * (barMaxValue/divNum);
        }
        else if (slider.value == barMaxValue)
        {
            Debug.Log("");
            disRunUI = 1;
            slider.value = 1f;
        }


        yield return new WaitForSeconds(disDelay);
        addingDis = false;

    }

   public void UpdateStarUI()
    {

        //���� ȹ���� ���
        // �� �̹��� ��ü
        for (int i = 0; i < starsToCollect; i++)
        {
            if (i < starsCollected)
            {
                // ȹ���� ���� ��� ��������� ����
                starImages[i].sprite = yellowStarSprite;
                EndstarImages[i].sprite = yellowStarSprite;
            }
            else
            {
                // ȹ������ ���� ���� ȸ������ ����
                starImages[i].sprite = greyStarSprite;
                EndstarImages[i].sprite= greyStarSprite;
            }

            //ȹ��� ����
            Debug.Log("star[i]" + starImages[i]);
            starImages[i].transform.DOShakePosition(duration, strength, vibrato);
        }
       

        // ��� ���� ȹ���� ��� �޽��� ���
        if (starsCollected == starsToCollect)
        {
            Debug.Log("��� ���� ȹ���߽��ϴ�!");
            //���� ��� ������ ��������
           playerMoveScript.ColletedAll();

        }
    }

}
