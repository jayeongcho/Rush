using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float leftRightSpeed = 4;
    void Update()
    {
        //월드 기준 이동 .앞
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        //좌측이동
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
            }
        }

        //우측이동
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed * -1);
            }
        }

    }
}

