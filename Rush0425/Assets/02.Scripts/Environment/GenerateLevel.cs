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
        secNum = Random.Range(0, 3);
        GameObject newSection = Instantiate(section[secNum],new Vector3(0,0,zPos),Quaternion.identity);
        
        zPos += 63; //�ʱⰪ50
        yield return new WaitForSeconds(createTime);
        creatingSection = false;

        // 60�� �ڿ� ������ ������Ʈ �ı�
        yield return new WaitForSeconds(60f);
        Destroy(newSection);
    }



}
