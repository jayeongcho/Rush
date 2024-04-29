using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked; //Default value is false;
    public Image unlockImage; //�ڹ����̹���
    public GameObject[] stars; //���̹���
    public Sprite starSprite;


    public int levelIndex; // �ش� ��ũ��Ʈ�� ����� ������Ʈ�� ���� �ε���


    // Start is called before the first frame update
    void Start()
    {
        // ���� ������ �ʱ�ȭ
        GameDataManager.InitializeLevelData(10); // ����: �� 10���� ������ �ִٰ� ����
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLevelImage();
        UpdateLevelStatus();

    }

    //���� ��������
    //private void UpdateLevelStatus()
    //{
    //    //  if the current lv is 5, the pre should be 4
    //    int previousLevelNum = int.Parse(gameObject.name) - 1;

    //    if (PlayerPrefs.GetInt("Lv" + previousLevelNum.ToString()) > 0)//If the firts level star is bigger than 0, second level can play

    //    {
    //        unlocked = true;
    //    }


    //}

    private void UpdateLevelStatus()
    {
        int currLevel = levelIndex;
        Debug.Log("currLevel" + currLevel);
        // �̹� ��� ������ �����̸� �� �̻� �� �ʿ� ����
        if (GameDataManager.GetLevelData(currLevel).unlocked)
        {
            Debug.Log("return");
            return;
        }

        int previousLevel = currLevel - 1;
        
        // ���� ������ ��� ������ ���¶�� ���� ������ ��� �����Ѵ�
        if (GameDataManager.GetLevelData(previousLevel).unlocked)
        {
            Debug.Log("previousLevel" + previousLevel);
            GameDataManager.SetLevelUnlocked(currLevel, true);
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

    // ���� �̹��� ������Ʈ
    //private void UpdateLevelImage()
    //{
    //    LevelData levelData = GameDataManager.GetLevelData(levelIndex);

    //    if (!levelData.unlocked) // ��� ����
    //    {
    //        unlockImage.gameObject.SetActive(true); // �ڹ��� �̹��� Ȱ��ȭ
    //        foreach (var star in stars)
    //        {
    //            star.SetActive(false); // �� �̹��� ��Ȱ��ȭ
    //        }
    //    }
    //    else // ��� ���� ����
    //    {
    //        unlockImage.gameObject.SetActive(false); // �ڹ��� �̹��� ��Ȱ��ȭ
    //        for (int i = 0; i < levelData.starsCollected; i++)
    //        {
    //            stars[i].SetActive(true); // �� �̹��� Ȱ��ȭ
    //            stars[i].GetComponent<Image>().sprite = starSprite; // �� �̹��� ��������Ʈ ����
    //        }
    //    }
    //}

    public void PressSelection(string _LevelName)//When we press this level, we can move to the corresponding Scene to play
    {
        if (unlocked)
        //if (GameDataManager.GetLevelData(levelIndex).unlocked)
        {
            Debug.Log(_LevelName);
            SceneManager.LoadScene(_LevelName);
        }
    }
}
