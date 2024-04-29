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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLevelImage();
        UpdateLevelStatus();
 ;    }

    private void UpdateLevelStatus()
    {
        //if the current lv is 5, the pre should be 4
        int previousLevelNum = int.Parse(gameObject.name) - 1;

        if (PlayerPrefs.GetInt("Lv" + previousLevelNum.ToString()) > 0)//If the firts level star is bigger than 0, second level can play
        {
            unlocked = true;
        }

    }
    
    private void UpdateLevelImage()
    {
        if (unlocked == false) //if unlock is false means This level is clocked!
        {
            unlockImage.gameObject.SetActive(true);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(false);
            }
        }
        else //if unlock is true means This level can play!
        {
            unlockImage.gameObject.SetActive(false);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < PlayerPrefs.GetInt("Lv" + gameObject.name); i++)
            {
                stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
            }
        }
    }

    public void PressSelection(string _LevelName)//When we press this level, we can move to the corresponding Scene to play
    {
        if (unlocked)
        {
            Debug.Log(_LevelName);
            SceneManager.LoadScene(_LevelName);
        }
    }
}
