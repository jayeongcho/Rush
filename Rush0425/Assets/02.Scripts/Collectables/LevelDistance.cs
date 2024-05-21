using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelDistance : MonoBehaviour
{
    //거리UI
   // public GameObject disDisplay;
   // public GameObject disEndDisplay;
    public int disRun;
    public bool addingDis = false;
    public float disDelay = 0.35f; //1m를 0.35초마다 돌파
    public float divNum ;
    // 별UI
    public Image [] starImages; //별이미지
    public Sprite yellowStarSprite; //노란별
    public Sprite greyStarSprite;//회색별
    public int starsCollected = 0;
    public int starsToCollect = 3;

    //게임끝나고 나올 별
    public Image[] EndstarImages; //별이미지 

    //레벨
    public int level;

    //성공시
    public GameObject thePlayer;
    private Toony_PlayerMove playerMoveScript;

    //슬라이더
    public Slider slider;
    float barMaxValue =100f;
    public float disRunUI;

    //흔들기
    public float duration = 3f; // 흔들기 지속 시간
    public float strength = 0.5f; // 흔들기 강도
    public int vibrato = 10; // 흔들기 횟수

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

    //m마다 거리추가
    IEnumerator AddingDis()
    {
        disRun += 1;
       // disDisplay.GetComponent<Text>().text = "" + disRun; //게임 중 거리 텍스트 
        //disEndDisplay.GetComponent<Text>().text = "" + disRun; //게임 끝나고 나오는 텍스트

        // 별 획득 체크
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

        //별을 획득한 경우
        // 별 이미지 교체
        for (int i = 0; i < starsToCollect; i++)
        {
            if (i < starsCollected)
            {
                // 획득한 별인 경우 노란색으로 변경
                starImages[i].sprite = yellowStarSprite;
                EndstarImages[i].sprite = yellowStarSprite;
            }
            else
            {
                // 획득하지 않은 별은 회색으로 유지
                starImages[i].sprite = greyStarSprite;
                EndstarImages[i].sprite= greyStarSprite;
            }

            //획득시 흔들기
            Debug.Log("star[i]" + starImages[i]);
            starImages[i].transform.DOShakePosition(duration, strength, vibrato);
        }
       

        // 모든 별을 획득한 경우 메시지 출력
        if (starsCollected == starsToCollect)
        {
            Debug.Log("모든 별을 획득했습니다!");
            //별을 모두 모으면 게임종료
           playerMoveScript.ColletedAll();

        }
    }

}
