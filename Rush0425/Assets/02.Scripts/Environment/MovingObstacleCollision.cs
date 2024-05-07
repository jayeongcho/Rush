using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingObstacleCollision : MonoBehaviour
{
    //����������
    public float speed;
    public float detectionRange = 5f; // �÷��̾� ���� ����
    bool playerInRange = false; // �÷��̾� ���� ����


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
        // �ڽ� ������Ʈ�� Ž���ϸ鼭 Ȱ��ȭ�� ������Ʈ�� ã��
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
            //���� ������
            moving();
        }

        //�����۸����� �ݶ��̴� ��Ȱ��/Ȱ��        
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
        // ���� ���� ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activeChild != null && other.CompareTag("Player"))
        {
            //�����̵� ���� 
            playerInRange = false;
            speed = 0f;

            
            ////�ε����� player �̵����߰�, �ش� ������Ʈ�� ���� 
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            //thePlayer.GetComponent<Toony_PlayerMove>().enabled = false;
            //charModel.GetComponent<Animator>().Play("Stumble Backwards");           
            //activeChild.GetComponent<Animator>().Play("Stumble Backwards");
            //levelControl.GetComponent<LevelDistance>().enabled = false;
            // crashThud.Play();
            //mainCam.GetComponent<Animator>().enabled = true;

            ////���ӳ���ȭ�� ����
            //levelControl.GetComponent<EndRunSequence>().enabled = true;


            //�ε����� player���� �޼��� ȣ��
            playerMoveScript.Crushed();

            
        }
    }
}
