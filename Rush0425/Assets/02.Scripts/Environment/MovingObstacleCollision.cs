using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingObstacleCollision : MonoBehaviour
{
    //버스움직임
    public float speed;
    public float detectionRange = 5f; // 플레이어 감지 범위
    bool playerInRange = false; // 플레이어 감지 여부


    public GameObject thePlayer;
    private Toony_PlayerMove playerMoveScript;
    //public GameObject charModel;
    public AudioSource crashThud;
    public GameObject mainCam;
    public GameObject levelControl;
    GameObject activeChild = null;
    BoxCollider boxcollider;

    void Start()
    {
        boxcollider = GetComponent<BoxCollider>();
        playerMoveScript = thePlayer.GetComponent<Toony_PlayerMove>();
        // 자식 오브젝트를 탐색하면서 활성화된 오브젝트를 찾음
        if (SceneManager.GetActiveScene().buildIndex != 0)
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

    }


    void Update()
    {
        if (playerInRange)
        {
            //버스 움직임
            moving();
        }

        //아이템먹으면 콜라이더 비활성/활성        
        if (playerMoveScript.moveSpeed > 9)
        {

            DisableCollider();
        }
        else
        {
            EnableCollider();
        }



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

    public void moving()
    {

        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
    public void EnableCollider()
    {
        boxcollider.enabled = true;
    }

    public void DisableCollider()
    {
        boxcollider.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        // 감지 범위 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activeChild != null && other.CompareTag("Player"))
        {
            //버스이동 멈춤 
            playerInRange = false;
            speed = 0f;

            
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
    }
}
