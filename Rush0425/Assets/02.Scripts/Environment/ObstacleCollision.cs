using System.Collections;
using UnityEngine;

//장애물이 플레이어와 부딪히면 실행
public class ObstacleCollision : MonoBehaviour
{
    public GameObject thePlayer;
    private Toony_PlayerMove playerMoveScript;
    // public GameObject charModel;
    public AudioSource crashThud;
    public GameObject mainCam;
    public GameObject levelControl;

    public GameObject activeChild = null;

    public bool isDie = false;
    BoxCollider boxcollider;
    Vector3 playerPosition;
    Vector3 offset = new Vector3(0, 0, 3f);

    void Start()
    {  // 자식 오브젝트를 탐색하면서 활성화된 오브젝트를 찾음
        Invoke("test", 0.01f);
        boxcollider = GetComponent<BoxCollider>();
        playerMoveScript = thePlayer.GetComponent<Toony_PlayerMove>();
        playerPosition = thePlayer.transform.position;

    }
    private void Update()
    {
        //아이템먹으면 콜라이더 비활성/활성      
        if (playerMoveScript.moveSpeed > 9)
        {
            Debug.Log(playerMoveScript.moveSpeed);
            DisableCollider();
        }
        else
        {
            EnableCollider();
        }
    }

    public void test()
    {
        for (int i = 0; i < thePlayer.transform.childCount; i++)
        {
            GameObject child = thePlayer.transform.GetChild(i).gameObject;
            if (child.activeSelf)
            {
                activeChild = child;
                break;
            }
        }
    }

    public void EnableCollider()
    {
        boxcollider.enabled = true;
    }

    public void DisableCollider()
    {
        boxcollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {


        /*if (!ItemProperties.getRespawnItem&&activeChild != null&& !isDie)
        //{
        //    isDie = true;
        //    //부딪히면 player 이동멈추고, 해당 컴포넌트들 해제 
        //    this.gameObject.GetComponent<BoxCollider>().enabled = false;
        //    thePlayer.GetComponent<Toony_PlayerMove>().enabled = false;
        //    // charModel.GetComponent<Animator>().Play("Stumble Backwards");
        //    activeChild.GetComponent<Animator>().Play("Stumble Backwards");
        //    levelControl.GetComponent<LevelDistance>().enabled = false;
        //    crashThud.Play();
        //    mainCam.GetComponent<Animator>().enabled = true;

        //    //게임끝난화면 실행
        //    levelControl.GetComponent<EndRunSequence>().enabled = true;
        //    playerPosition = thePlayer.transform.position;


        //}*/
        if (!ItemProperties.getRespawnItem && activeChild != null && !isDie && other.CompareTag("Player"))
        {
            ////부딪히면 player 이동멈추고, 해당 컴포넌트들 해제 
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            //thePlayer.GetComponent<Toony_PlayerMove>().enabled = false;
            //charModel.GetComponent<Animator>().Play("Stumble Backwards");           
            //activeChild.GetComponent<Animator>().Play("Stumble Backwards");
            //levelControl.GetComponent<LevelDistance>().enabled = false;
            // crashThud.Play();
            //mainCam.GetComponent<Animator>().enabled = true;

            ////게임끝난화면 실행
            //levelControl.GetComponent<EndRunSequence>().enabled = true;


            //부딪히면 player에서 메서드 호출
            playerMoveScript.Crushed();


        }


        //부활하기
        if (ItemProperties.getRespawnItem && activeChild != null && !isDie)
        
        {
            Debug.Log("ItemProperties.getRespawnItem" + ItemProperties.getRespawnItem.ToString());
            Respawn();
            ItemProperties.getRespawnItem = false;
        }

    }
    public void Respawn()
    {
        if (activeChild != null)
        {
            Debug.Log("ObstacleCOllision's Respawn");
            //부딪히면 player 이동멈추고, 해당 컴포넌트들 해제 
            // this.gameObject.GetComponent<BoxCollider>().enabled = false;
            thePlayer.GetComponent<Toony_PlayerMove>().enabled = false;
            // charModel.GetComponent<Animator>().Play("Stumble Backwards");
            activeChild.GetComponent<Animator>().Play("Stumble Backwards");
            levelControl.GetComponent<LevelDistance>().enabled = false;
            crashThud.Play();
            mainCam.GetComponent<Animator>().enabled = true;

            Invoke("InvokeRespawn", 3f);

            
        }
    }
    void InvokeRespawn()
    {

        // this.gameObject.GetComponent<BoxCollider>().enabled = true;
        thePlayer.GetComponent<Toony_PlayerMove>().enabled = true;
        StartCoroutine(charactercontroller());
        activeChild.GetComponent<Animator>().Play("Standard Run"); //다시 뛰기
        levelControl.GetComponent<LevelDistance>().enabled = true;

        mainCam.GetComponent<Animator>().enabled = false;
        playerPosition = thePlayer.transform.position + offset;
        thePlayer.transform.position = playerPosition;
        
    }

    IEnumerator charactercontroller()
    {
        thePlayer.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(1f);
        thePlayer.GetComponent<CharacterController>().enabled = true;
    }
}



