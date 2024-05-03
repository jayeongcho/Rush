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
        secNum = Random.Range(0, 3);
        GameObject newSection = Instantiate(section[secNum],new Vector3(0,0,zPos),Quaternion.identity);
        
        zPos += 63; //초기값50
        yield return new WaitForSeconds(createTime);
        creatingSection = false;

        // 60초 뒤에 생성된 오브젝트 파괴
        yield return new WaitForSeconds(60f);
        Destroy(newSection);
    }



}
