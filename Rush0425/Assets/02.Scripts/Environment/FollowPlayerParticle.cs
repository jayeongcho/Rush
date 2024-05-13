using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerParticle : MonoBehaviour
{
    public Transform playerTransform; // �÷��̾��� Transform
     Vector3 offset = new Vector3(0, 50, 0);

    void Update()
    {
        if (playerTransform != null)
        {
           
            // ��ƼŬ �ý����� ��ġ�� �÷��̾��� ��ġ�� �����մϴ�.
            transform.position = playerTransform.position +offset;
           
        }
        else
        {
            // �÷��̾� Transform�� ������ ��ƼŬ �ý����� ��Ȱ��ȭ�մϴ�.
            gameObject.SetActive(false);
        }
    }
}
