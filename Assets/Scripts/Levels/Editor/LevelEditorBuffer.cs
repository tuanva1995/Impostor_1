using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelEditorBuffer
{
    public static ObstacleData[] ObstacleBuffer;
    public static TrapDataLevel[] TrapDataBuffer;

    public static void CopyObstacle(ObstacleData[] datas)
    {
        ObstacleBuffer = datas;
    }

    public static void CopyTrapData(TrapDataLevel[] datas)
    {
        TrapDataBuffer = datas;
    }
}
