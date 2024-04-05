using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section;
    public int zPos = 50; //1section 길이
    public bool creatingSection = false;
    public int secNum;

    void Update()
    {
        //발판 생성 체크
        if(creatingSection == false)
        {
            creatingSection = true; 
            StartCoroutine(GenerateSection());
        }
    }

    //2초마다 새로운 발판 생성
    IEnumerator GenerateSection()
    {
        secNum = Random.Range(0, 3);
        Instantiate(section[secNum],new Vector3(0,0,zPos),Quaternion.identity);
        zPos += 50;
        yield return new WaitForSeconds(2);
        creatingSection = false;
    }
}
