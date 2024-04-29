using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked; //Default value is false;
    public Image unlockImage; //자물쇠이미지
    public GameObject[] stars; //별이미지
    public Sprite starSprite;


    public int levelIndex; // 해당 스크립트가 적용된 오브젝트의 레벨 인덱스


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLevelImage();
      UpdateLevelStatus();
    
    }

    //레벨 가져오기
    //private void UpdateLevelStatus()
    //{
    //  //  if the current lv is 5, the pre should be 4
    //    int previousLevelNum = int.Parse(gameObject.name) - 1;

    //    if (PlayerPrefs.GetInt("Lv" + previousLevelNum.ToString()) > 0)//If the firts level star is bigger than 0, second level can play

    //    {
    //        unlocked = true;
    //    }


    //}

    // 레벨 상태 업데이트
    private void UpdateLevelStatus()
    {
        // 이전 레벨 번호 계산 (현재 레벨의 이전 레벨)
        int previousLevelNum = levelIndex - 1;

        // 이전 레벨이 존재하고, 이전 레벨이 잠금 해제되었으면 현재 레벨을 잠금 해제 상태로 설정
        if (previousLevelNum >= 0 && GameDataManager.GetLevelData(previousLevelNum).unlocked)
        {
            GameDataManager.SetLevelUnlocked(levelIndex, true);
        }
    }


    //private void UpdateLevelImage()
    //{
    //    if (unlocked == false) //if unlock is false means This level is clocked!
    //    {
    //        unlockImage.gameObject.SetActive(true);
    //        for (int i = 0; i < stars.Length; i++)
    //        {
    //            stars[i].gameObject.SetActive(false);
    //        }
    //    }
    //    else //if unlock is true means This level can play!
    //    {
    //        unlockImage.gameObject.SetActive(false);
    //        for (int i = 0; i < stars.Length; i++)
    //        {
    //            stars[i].gameObject.SetActive(true);
    //        }
    //        for (int i = 0; i < PlayerPrefs.GetInt("Lv" + gameObject.name); i++)
    //        {
    //            stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
    //        }
    //    }
    //}

    // 레벨 이미지 업데이트
    private void UpdateLevelImage()
    {
        LevelData levelData = GameDataManager.GetLevelData(levelIndex);

        if (!levelData.unlocked) // 잠금 상태
        {
            unlockImage.gameObject.SetActive(true); // 자물쇠 이미지 활성화
            foreach (var star in stars)
            {
                star.SetActive(false); // 별 이미지 비활성화
            }
        }
        else // 잠금 해제 상태
        {
            unlockImage.gameObject.SetActive(false); // 자물쇠 이미지 비활성화
            for (int i = 0; i < levelData.starsCollected; i++)
            {
                stars[i].SetActive(true); // 별 이미지 활성화
                stars[i].GetComponent<Image>().sprite = starSprite; // 별 이미지 스프라이트 설정
            }
        }
    }
    public void PressSelection(string _LevelName)//When we press this level, we can move to the corresponding Scene to play
    {
        //  if (unlocked)
        if (GameDataManager.GetLevelData(levelIndex).unlocked)
        {
            Debug.Log(_LevelName);
            SceneManager.LoadScene(_LevelName);
        }
    }
}
