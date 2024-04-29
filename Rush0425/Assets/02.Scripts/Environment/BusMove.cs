using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMove : MonoBehaviour
{
    public float speed;
    public float detectionRange = 5f; // �÷��̾� ���� ����
    PlayerMove playerMove;
    public bool playerInRange = false; // �÷��̾� ���� ����

    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>(); // PlayerMove ��ũ��Ʈ ã��
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
        // �÷��̾� ����
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
        // ���� ���� ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
