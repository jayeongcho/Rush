using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float leftRightSpeed = 4;
    static public bool canMove = false;

    public bool isJumping = false;
    public bool comingDown = false;
    public GameObject playerObject; //게임플레이어

    void Update()
    {
        //월드 기준 앞으로 이동 
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        if (canMove == true)
        {
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
            //점프
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            {
                if (isJumping == false)
                {
                    isJumping = true;
                    playerObject.GetComponent<Animator>().Play("Jump");
                    //점프코루틴
                    StartCoroutine(JumpSequence());
                }


            }
        }
        if (isJumping == true)
        {
            if (comingDown == false)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 5, Space.World);

            }
            if (comingDown == true)
            {
                transform.Translate(Vector3.up * Time.deltaTime * -5, Space.World);
            }
        }

    }

    IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(0.45f);
        comingDown = true;
        yield return new WaitForSeconds(0.45f);
        isJumping = false;
        comingDown = false;
        playerObject.GetComponent<Animator>().Play("Standard Run");
    }
}

