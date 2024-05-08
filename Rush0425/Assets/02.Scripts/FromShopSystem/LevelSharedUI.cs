using TMPro;
using UnityEngine;

public class LevelSharedUI : MonoBehaviour
{
    //화면에 동전 수를 나타내는 텍스트 배열
    [SerializeField] TMP_Text[] LevelUIText;
    public int level;
   
    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelUIText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLevelUIText()
    {
        if (level != 0)
        {
            //동전 텍스트 배열을 반복하여 업데이트
            for (int i = 0; i < LevelUIText.Length; i++)
            {
                SetLevelText(LevelUIText[i], level);
            }
        }
    }

    void SetLevelText(TMP_Text textMesh, int value)
    {
        textMesh.text = value.ToString();
    }
}
