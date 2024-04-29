using System.Collections;
using UnityEngine;

public class Toony_PlayerMove : MonoBehaviour
{

    [SerializeField] GameObject[] characterPrefabs;
    [SerializeField] Character character;
    [SerializeField] CharacterShopDatabase characterDB;
    [SerializeField] ObstacleCollision collison;
    public float moveSpeed;
    public float leftRightSpeed = 4;
    static public bool canMove = false;

    public bool isJumping = false;
    public bool comingDown = false;
    static public bool isdoubleCoin = false;
    public GameObject playerObject; //게임플레이어

    public float detectionRange = 10f; // 플레이어 감지 범위



    private void Awake()
    {
        ChangePlayerSkin();

    }
    void Start()
    {
    }
    void ChangePlayerSkin()
    {
        Character character = GameDataManager.GetSelectedCharacter();
        int selectedSkin = GameDataManager.GetSelectedCharacterIndex();

        characterPrefabs[selectedSkin].SetActive(true);
        playerObject = characterPrefabs[selectedSkin];
        Debug.Log("characterPrefabs[selectedSkin]" + characterPrefabs[selectedSkin]);

        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            if (i != selectedSkin)
            { characterPrefabs[i].SetActive(false); }
        }
        moveSpeed =
         //  characterDB.GetCharacter(selectedSkin).speed;
         GameDataManager.GetcharacterSpeed();

    }


    void Update()
    {
        //월드 기준 앞으로 이동 
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        //transform.Translate(Vector3.forward * Time.deltaTime * character.speed, Space.World);


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