using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject_Snow : MonoBehaviour
{
    public int rotateSpeed = 1;
    
    private void Start()
    {
        
    }
    void Update()
    {

         transform.Rotate(0, 0 , rotateSpeed, Space.World);
       

       
    }
}
