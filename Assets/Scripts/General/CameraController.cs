using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float defRatio = 16f / 9f;
    void Start()
    {

        float aspectRatio = (float)Screen.height / (float)Screen.width;
        //GetComponent<Camera>().fieldOfView = 82;
    }
    int index = 1;
    bool isZoom = false;
    float timeZoom = 3;
    public void SetField()
    {
        timeZoom = 0;


    }
    private void Update()
    {
        if ( timeZoom < 1)
        {
            timeZoom += Time.deltaTime;
            GetComponent<Camera>().fieldOfView += Time.deltaTime*3;
        }
       
    }
}
