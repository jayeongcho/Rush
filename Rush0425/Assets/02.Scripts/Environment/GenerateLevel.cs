using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section;
    public int zPos = 63; //1section 길이
    public bool creatingSection = false;
    public int secNum;
    public int createTime =3;
    void Update()
    {
        //발판 생성 체크
        if(creatingSection == false)
        {
            //발판생성
            creatingSection = true; 
            StartCoroutine(GenerateSection());
        }
    }

    //2초마다 새로운 발판 생성
    IEnumerator GenerateSection()
    {
       // secNum = Random.Range(0, 3);

        // 확률에 따라 발판 선택
        int selectedSectionIndex = GetRandomSectionIndex();
       
        GameObject newSection = Instantiate(section[selectedSectionIndex],new Vector3(0,0,zPos),Quaternion.identity);
        
        zPos += 63; //초기값50
        yield return new WaitForSeconds(createTime);
        creatingSection = false;

        // 60초 뒤에 생성된 오브젝트 파괴
        yield return new WaitForSeconds(120f);
        Destroy(newSection);
    }

    // 발판 확률에 따라 인덱스 선택
    int GetRandomSectionIndex()
    {
        // 발판의 확률 설정 (10%, 30%, 30%, 30%)
        int[] probabilities = { 10, 30, 30, 30 };

        // 확률에 따라 인덱스 선택
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

        // 만약 여기까지 도달하면 오류이므로 마지막 인덱스 반환
        return probabilities.Length - 1;
    }

}
