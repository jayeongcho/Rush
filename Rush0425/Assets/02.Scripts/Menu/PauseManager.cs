using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
   public bool isPause = false;
  
    // Start is called before the first frame update
    void Start()
    {  isPause = false;
    Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pause()
    {
       isPause = true;
         Time.timeScale = 0f; 
        
       
    }

    public void Resume()
    {
        isPause = false;
        Time.timeScale = 1f;
        
    }
}
