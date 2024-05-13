using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section;
    public int zPos = 63; //1section ����
    public bool creatingSection = false;
    public int secNum;
    public int createTime =3;
    void Update()
    {
        //���� ���� üũ
        if(creatingSection == false)
        {
            //���ǻ���
            creatingSection = true; 
            StartCoroutine(GenerateSection());
        }
    }

    //2�ʸ��� ���ο� ���� ����
    IEnumerator GenerateSection()
    {
       // secNum = Random.Range(0, 3);

        // Ȯ���� ���� ���� ����
        int selectedSectionIndex = GetRandomSectionIndex();
       
        GameObject newSection = Instantiate(section[selectedSectionIndex],new Vector3(0,0,zPos),Quaternion.identity);
        
        zPos += 63; //�ʱⰪ50
        yield return new WaitForSeconds(createTime);
        creatingSection = false;

        // 60�� �ڿ� ������ ������Ʈ �ı�
        yield return new WaitForSeconds(120f);
        Destroy(newSection);
    }

    // ���� Ȯ���� ���� �ε��� ����
    int GetRandomSectionIndex()
    {
        // ������ Ȯ�� ���� (10%, 30%, 30%, 30%)
        int[] probabilities = { 10, 30, 30, 30 };

        // Ȯ���� ���� �ε��� ����
        int totalProbability = 0;
        for (int i = 0; i < probabilities.Length; i++)
        {
            totalProbability += probabilities[i];
        }

        int randomValue = Random.Range(0, totalProbability);
        int accumulatedProbability = 0;

        for (int i = 0; i < probabilities.Length; i++)
        {
            accumulatedProbability += probabilities[i];
            if (randomValue < accumulatedProbability)
            {
                return i;
            }
        }

        // ���� ������� �����ϸ� �����̹Ƿ� ������ �ε��� ��ȯ
        return probabilities.Length - 1;
    }

}
