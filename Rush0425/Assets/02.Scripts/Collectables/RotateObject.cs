using UnityEngine;


//오브젝트 회전 스크립트
public class RotateObject : MonoBehaviour
{
    

    public int rotateSpeed = 1;

    void Update()
    {
        
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
