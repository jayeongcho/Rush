using System.Collections;
using UnityEngine;

public class Toony_PlayerMove : MonoBehaviour
{

    [SerializeField] GameObject[] characterPrefabs;
    // [SerializeField] Character character;
    [SerializeField] CharacterShopDatabase characterDB;
    [SerializeField] ObstacleCollision collison;
    public float moveSpeed;
    public float leftRightSpeed = 4;
    static public bool canMove = false;

    public bool isJumping = false;
    public bool comingDown = false;
    public bool isSlide = false;
    static public bool isdoubleCoin = false;
    public GameObject playerObject; //게임플레이어

    public float detectionRange = 10f; // 플레이어 감지 범위

    //모바일
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool isSwiping = false;

    public float swipeThreshold = 50f;

    //멈춤   
    public AudioSource crashThud;
    public GameObject mainCam;
    public GameObject levelControl;


    private void Awake()
    {
        ChangePlayerSkin();

    }
    void Start()
    {

    }
    void ChangePlayerSkin()
    {
        //Character character = GameDataManager.GetSelectedCharacter();
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
            //점프
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (isSlide == false)
                {
                    isSlide = true;
                    playerObject.GetComponent<Animator>().Play("Slide");
                    //점프코루틴
                    StartCoroutine(SlideSequence());
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

        //모바일
        // 터치 입력의 개수를 확인합니다.
        if (Input.touchCount > 0)
        {
            // 첫 번째 터치 입력을 가져옵니다.
            Touch touch = Input.GetTouch(0);

            // 터치 상태에 따라 다른 동작을 수행합니다.
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // 터치가 시작되면 시작 위치를 기록합니다.
                    fingerDownPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    // 터치가 종료되면 종료 위치를 기록하고 스와이프를 확인합니다.
                    fingerUpPosition = touch.position;
                    CheckSwipe();
                    isSwiping = false;
                    break;
            }
        }

        // 마우스 입력 처리
        if (Input.GetMouseButtonDown(0))
        {
            fingerDownPosition = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            fingerUpPosition = Input.mousePosition;
            CheckSwipe();
            isSwiping = false;
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

    IEnumerator SlideSequence()
    {

        yield return new WaitForSeconds(1f);
        isSlide = false;
        playerObject.GetComponent<Animator>().Play("Standard Run");
    }

    void CheckSwipe()
    {
        // 스와이프 거리를 계산합니다.
        float swipeDistanceX = Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
        float swipeDistanceY = Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);

        // 좌우 스와이프 확인
        if (isSwiping && swipeDistanceX > swipeThreshold && swipeDistanceX > swipeDistanceY)
        {
            if (fingerDownPosition.x - fingerUpPosition.x > 0)
            {
                // 플레이어를 x축으로 -2만큼 이동시킵니다.
                transform.Translate(Vector3.left * 2f);
                // 왼쪽으로 스와이프한 경우
                Debug.Log("왼쪽으로 스와이프");

            }
            else
            {
                transform.Translate(Vector3.right * 2f);
                // 오른쪽으로 스와이프한 경우
                Debug.Log("오른쪽으로 스와이프");
            }
        }
        // 상하 스와이프 확인
        else if (isSwiping && swipeDistanceY > swipeThreshold && swipeDistanceY > swipeDistanceX)
        {
            if (fingerDownPosition.y - fingerUpPosition.y > 0)
            {
                // 아래로 스와이프한 경우
                Debug.Log("아래로 스와이프");
            }
            else
            {
                isJumping = true;
                playerObject.GetComponent<Animator>().Play("Jump");
                StartCoroutine(JumpSequence());
                // 위로 스와이프한 경우
                Debug.Log("위로 스와이프");
            }
        }
    }

    //게임종료(별 모았을때)
    public void ColletedAll()
    {
        this.GetComponent<Toony_PlayerMove>().enabled = false;
        playerObject.GetComponent<Animator>().Play("Victory");
        levelControl.GetComponent<LevelDistance>().enabled = false;
        crashThud.Play(); //사운드 (바꿔야함!!!)
        //mainCam.GetComponent<Animator>().enabled = true; //카메라 다른애니메이션 넣기!

        levelControl.GetComponent<EndRunSequence>().enabled = true;
    }

    //게임종료(부딪혔을때)
    public void Crushed()
    {
        this.GetComponent<Toony_PlayerMove>().enabled = false;
        playerObject.GetComponent<Animator>().Play("Stumble Backwards");
        levelControl.GetComponent<LevelDistance>().enabled = false;
        crashThud.Play(); //사운드
        mainCam.GetComponent<Animator>().enabled = true;

        levelControl.GetComponent<EndRunSequence>().enabled = true;
    }
}