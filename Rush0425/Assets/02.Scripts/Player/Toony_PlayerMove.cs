using System.Collections;
using UnityEngine;

public class Toony_PlayerMove : MonoBehaviour
{

    [SerializeField] GameObject[] characterPrefabs;
    // [SerializeField] Character character;
    [SerializeField] CharacterShopDatabase characterDB;
    [SerializeField] ObstacleCollision collison;
    private LevelDistance levelDistanceScript;
    public float moveSpeed;
    public float leftRightSpeed = 4;
    static public bool canMove = false;
    public float accelspeed; //����


    public bool isJumping = false;
    public bool comingDown = false;
    public bool isSlide = false;
    static public bool isdoubleCoin = false;
    public GameObject playerObject; //�����÷��̾�

    public float detectionRange = 10f; // �÷��̾� ���� ����

    //�����
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool isSwiping = false;

    public float swipeThreshold = 50f;

    //����   
    public AudioSource crashThud;
    public GameObject mainCam;
    public GameObject levelControl;


    private void Awake()
    {
        ChangePlayerSkin();

    }
    void Start()
    {
        // LevelDistance ��ũ��Ʈ�� �ν��Ͻ��� ������
        levelDistanceScript = FindObjectOfType<LevelDistance>();
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
        //���� ���� ������ �̵� 
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        //transform.Translate(Vector3.forward * Time.deltaTime * character.speed, Space.World);
        //��1�� ������� 0.2�� ��������


        if (canMove == true)
        {
            //�����̵�
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
                }
            }

            //�����̵�
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed * -1);
                }
            }
            //����
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            {
                if (isJumping == false)
                {
                    isJumping = true;
                    playerObject.GetComponent<Animator>().Play("Jump");
                    //�����ڷ�ƾ
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

        //�����
        // ��ġ �Է��� ������ Ȯ���մϴ�.
        if (Input.touchCount > 0 && canMove == true)
        {
            // ù ��° ��ġ �Է��� �����ɴϴ�.
            Touch touch = Input.GetTouch(0);

            // ��ġ ���¿� ���� �ٸ� ������ �����մϴ�.
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // ��ġ�� ���۵Ǹ� ���� ��ġ�� ����մϴ�.
                    fingerDownPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    // ��ġ�� ����Ǹ� ���� ��ġ�� ����ϰ� ���������� Ȯ���մϴ�.
                    fingerUpPosition = touch.position;
                    CheckSwipe();
                    isSwiping = false;
                    break;
            }
        }

        // ���콺 �Է� ó��
        if (Input.GetMouseButtonDown(0) && canMove == true)
        {
            fingerDownPosition = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButtonUp(0) && canMove == true)
        {
            fingerUpPosition = Input.mousePosition;
            CheckSwipe();
            isSwiping = false;
        }

        //����
        // LevelDistance ��ũ��Ʈ���� ������ �����ͼ� ���� ȹ��
        //if (levelDistanceScript != null && levelDistanceScript.starsCollected >= levelDistanceScript.starsToCollect)
        //{
        //    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed*accelspeed, Space.World);
        //}
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
        // �������� �Ÿ��� ����մϴ�.
        float swipeDistanceX = Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
        float swipeDistanceY = Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);

        // �¿� �������� Ȯ��
        if (isSwiping && swipeDistanceX > swipeThreshold && swipeDistanceX > swipeDistanceY)
        {
            // �������� ���������� ���
            if (fingerDownPosition.x - fingerUpPosition.x > 0)
            {
                // �÷��̾ x������ -2��ŭ �̵���ŵ�ϴ�.
                transform.Translate(Vector3.left * 2f);

            }
            // ���������� ���������� ���
            else
            {
                transform.Translate(Vector3.right * 2f);
            }
        }
        // ���� �������� Ȯ��
        else if (isSwiping && swipeDistanceY > swipeThreshold && swipeDistanceY > swipeDistanceX)
        {
            if (fingerDownPosition.y - fingerUpPosition.y > 0)
            {


            }
            else
            {
                // ���� ���������� ��� ����
                isJumping = true;
                playerObject.GetComponent<Animator>().Play("Jump");
                StartCoroutine(JumpSequence());

            }
        }
    }

    //��������(�� �������)
    public void ColletedAll()
    {

        this.GetComponent<Toony_PlayerMove>().enabled = false;
        playerObject.GetComponent<Animator>().Play("Victory");
        levelControl.GetComponent<LevelDistance>().enabled = false;
        crashThud.Play(); //���� (�ٲ����!!!)
        //mainCam.GetComponent<Animator>().enabled = true; //ī�޶� �ٸ��ִϸ��̼� �ֱ�!

        levelControl.GetComponent<EndRunSequence>().enabled = true;
    }

    //��������(�ε�������)
    public void Crushed()
    {


        this.GetComponent<Toony_PlayerMove>().enabled = false;
        playerObject.GetComponent<Animator>().Play("Stumble Backwards");
        levelControl.GetComponent<LevelDistance>().enabled = false;
        crashThud.Play(); //����
        mainCam.GetComponent<Animator>().enabled = true;

        levelControl.GetComponent<EndRunSequence>().enabled = true;

    }
}