using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    //플레이어가 좌우로 움직이는 위치 제한
    public static float leftSide = -3.0f;
    public static float rightSide= 3.0f;
    public float internalLeft;
    public float internalRight;
    void Update()
    {
        internalLeft = leftSide;
        internalRight = rightSide;
    }
}
