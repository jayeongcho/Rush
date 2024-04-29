using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Vector3 lastCheckpointPosition;

    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
    }

    public Vector3 GetLastCheckpointPosition()
    {
        return lastCheckpointPosition;
    }
}
