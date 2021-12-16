using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public Transform[] points;
    public Transform[] playerPoint;
    public List<Transform> enmyPoint;
    public Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance._offScreenIndicator.SetStart(_camera);
    }
    public Vector3 GetPos(int index)
    {
        var rd = Random.Range(0, points.Length);
        while (rd == (index - 1))
        {
            rd = Random.Range(0, points.Length);
        }
        
        return points[rd].position;
    }

}
