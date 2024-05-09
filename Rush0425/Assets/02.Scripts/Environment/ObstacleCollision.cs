using System.Collections;
using UnityEngine;

//��ֹ��� �÷��̾�� �ε����� ����
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
    {  // �ڽ� ������Ʈ�� Ž���ϸ鼭 Ȱ��ȭ�� ������Ʈ�� ã��
        Invoke("test", 0.01f);
        boxcollider = GetComponent<BoxCollider>();
        playerMoveScript = thePlayer.GetComponent<Toony_PlayerMove>();
        playerPosition = thePlayer.transform.position;

    }
    private void Update()
    {
        //�����۸����� �ݶ��̴� ��Ȱ��/Ȱ��      
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
        //    //�ε����� player �̵����߰�, �ش� ������Ʈ�� ���� 
        //    this.gameObject.GetComponent<BoxCollider>().enabled = false;
        //    thePlayer.GetComponent<Toony_PlayerMove>().enabled = false;
        //    // charModel.GetComponent<Animator>().Play("Stumble Backwards");
        //    activeChild.GetComponent<Animator>().Play("Stumble Backwards");
        //    levelControl.GetComponent<LevelDistance>().enabled = false;
        //    crashThud.Play();
        //    mainCam.GetComponent<Animator>().enabled = true;

        //    //���ӳ���ȭ�� ����
        //    levelControl.GetComponent<EndRunSequence>().enabled = true;
        //    playerPosition = thePlayer.transform.position;


        //}*/
        if (!ItemProperties.getRespawnItem && activeChild != null && !isDie && other.CompareTag("Player"))
        {
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


        //��Ȱ�ϱ�
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
            //�ε����� player �̵����߰�, �ش� ������Ʈ�� ���� 
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
        activeChild.GetComponent<Animator>().Play("Standard Run"); //�ٽ� �ٱ�
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



