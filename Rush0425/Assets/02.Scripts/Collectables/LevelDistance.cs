using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    //거리UI
    public GameObject disDisplay;
    public GameObject disEndDisplay;
    public int disRun;
    public bool addingDis = false;
    public float disDelay = 0.35f; //1m를 0.35초마다 돌파
    
    // 별UI
    public Image [] starImages; //별이미지
    public Sprite yellowStarSprite; //노란별
    public Sprite greyStarSprite;//회색별
    public int starsCollected = 0;
    public int starsToCollect = 3;

    //레벨
    public int level;

    //성공시
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

    //m마다 거리추가
    IEnumerator AddingDis()
    {
        disRun += 1;
        disDisplay.GetComponent<Text>().text = "" + disRun; //게임 중 거리 텍스트 
        disEndDisplay.GetComponent<Text>().text = "" + disRun; //게임 끝나고 나오는 텍스트

        // 별 획득 체크
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

        //별을 획득한 경우
        // 별 이미지 교체
        for (int i = 0; i < starsToCollect; i++)
        {
            if (i < starsCollected)
            {
                // 획득한 별인 경우 노란색으로 변경
                starImages[i].sprite = yellowStarSprite;
            }
            else
            {
                // 획득하지 않은 별은 회색으로 유지
                starImages[i].sprite = greyStarSprite;
            }
        }
        // 모든 별을 획득한 경우 메시지 출력
        if (starsCollected == starsToCollect)
        {
            Debug.Log("모든 별을 획득했습니다!");
           playerMoveScript.ColletedAll();

        }
    }

}
