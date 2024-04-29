using System.Collections;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    public GameObject countDown3;
    public GameObject countDown2;
    public GameObject countDown1;
    public GameObject countDownGo;
    public GameObject fadeIn;
    public AudioSource readyFx;
    public AudioSource goFx;
    void Start()
    {
        StartCoroutine(CountSequence());

    }

    IEnumerator CountSequence()
    {
        yield return new WaitForSeconds(1.5f);
        countDown3.SetActive(true);
        readyFx.Play();
        yield return new WaitForSeconds(1f);
        countDown2.SetActive(true);
        readyFx.Play();
        yield return new WaitForSeconds(1f);
        countDown1.SetActive(true);
        readyFx.Play();
        yield return new WaitForSeconds(1f);
        countDownGo.SetActive(true);
        goFx.Play();

        //ī��Ʈ �ٿ��� ������ ������ �� ����
        //PlayerMove.canMove = true;
        Toony_PlayerMove.canMove = true;
    }
}
