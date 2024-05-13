using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerParticle : MonoBehaviour
{
    public Transform playerTransform; // 플레이어의 Transform
     Vector3 offset = new Vector3(0, 50, 0);

    void Update()
    {
        if (playerTransform != null)
        {
           
            // 파티클 시스템의 위치를 플레이어의 위치로 설정합니다.
            transform.position = playerTransform.position +offset;
           
        }
        else
        {
            // 플레이어 Transform이 없으면 파티클 시스템을 비활성화합니다.
            gameObject.SetActive(false);
        }
    }
}
