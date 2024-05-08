using TMPro;
using UnityEngine;

public class LevelSharedUI : MonoBehaviour
{
    //ȭ�鿡 ���� ���� ��Ÿ���� �ؽ�Ʈ �迭
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
            //���� �ؽ�Ʈ �迭�� �ݺ��Ͽ� ������Ʈ
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
