using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    //�Ÿ�UI
    public GameObject disDisplay;
    public GameObject disEndDisplay;
    public int disRun;
    public bool addingDis = false;
    public float disDelay = 0.35f; //1m�� 0.35�ʸ��� ����
    
    // ��UI
    public Image [] starImages; //���̹���
    public Sprite yellowStarSprite; //�����
    public Sprite greyStarSprite;//ȸ����
    public int starsCollected = 0;
    public int starsToCollect = 3;

    //����
    public int level;

    //������
    public GameObject thePlayer;
    private Toony_PlayerMove playerMoveScript;

    private void Start()
    {
        playerMoveScript = thePlayer.GetComponent<Toony_PlayerMove>();
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
        disDisplay.GetComponent<Text>().text = "" + disRun; //���� �� �Ÿ� �ؽ�Ʈ 
        disEndDisplay.GetComponent<Text>().text = "" + disRun; //���� ������ ������ �ؽ�Ʈ

        // �� ȹ�� üũ
        if (disRun % 10 == 0)
        {
            starsCollected++;
            
            PlayerPrefs.SetInt("Lv" + level, starsCollected);
           
            UpdateStarUI();
           
        }

        yield return new WaitForSeconds(disDelay);
        addingDis = false;

    }

    void UpdateStarUI()
    {

        //���� ȹ���� ���
        // �� �̹��� ��ü
        for (int i = 0; i < starsToCollect; i++)
        {
            if (i < starsCollected)
            {
                // ȹ���� ���� ��� ��������� ����
                starImages[i].sprite = yellowStarSprite;
            }
            else
            {
                // ȹ������ ���� ���� ȸ������ ����
                starImages[i].sprite = greyStarSprite;
            }
        }
        // ��� ���� ȹ���� ��� �޽��� ���
        if (starsCollected == starsToCollect)
        {
            Debug.Log("��� ���� ȹ���߽��ϴ�!");
           playerMoveScript.ColletedAll();

        }
    }

}
