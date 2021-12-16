using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObstacleData
{
    public int obstacleID;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
}

[System.Serializable]
public struct TrapDataLevel
{
    public TrapKind trapKind;
    public Vector3 BeginPos;
    [ConditionalField(nameof(trapKind), false, TrapKind.SkullBall)] public Vector3 TargetPos;
    public Vector3 TrapRotation;
}

public class CreateLevelData : MonoBehaviour
{
    public int levelID;
    public float timeLevel;
    public int beginDiamond;
    public int maxDiamond;
    public int mapID;
    [ConditionalField(nameof(mapID), false, 3)] public bool isActiveTorus;
    public ObstacleData[] Obstacles;
    public Vector3 PlayerPosition;
    public Vector3[] BotPositions;
    public TrapDataLevel[] TrapDatas;

    private void Awake()
    {
        LevelManager.AddLevel(this);
    }
}
