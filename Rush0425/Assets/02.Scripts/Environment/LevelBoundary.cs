using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    //�÷��̾ �¿�� �����̴� ��ġ ����
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
