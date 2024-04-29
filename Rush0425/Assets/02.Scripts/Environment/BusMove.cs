using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMove : MonoBehaviour
{
    public float speed;
    public float detectionRange = 5f; // 플레이어 감지 범위
    PlayerMove playerMove;
    public bool playerInRange = false; // 플레이어 감지 여부

    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>(); // PlayerMove 스크립트 찾기
    }

    void Update()
    {
        if (playerInRange)
        {
            moving();
        }
    }

    public void moving()
    {

        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

  
    void FixedUpdate()
    {
        // 플레이어 감지
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerInRange = true;
                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // 감지 범위 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
