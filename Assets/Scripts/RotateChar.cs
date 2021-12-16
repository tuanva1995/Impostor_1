using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChar : MonoBehaviour
{
    float rotSpeed = 15;
    Quaternion oriRot;
    bool isDrag;
    float time;
    private void Awake()
    {
        oriRot = transform.rotation;
    }
    private void OnMouseDrag()
    {
        isDrag = true;
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        transform.RotateAround(Vector3.up, -rotX);
        //float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
        //transform.RotateAround(Vector3.right, -rotY);
    }
    private void OnMouseUp()
    {
        isDrag = false;
    }
    private void Update()
    {
        if (!isDrag)
        {
            time += Time.deltaTime;
        }
        else time = 0;
        if(transform.rotation != oriRot && time > 2f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, oriRot, rotSpeed * Time.deltaTime * 5);
        }
    }
}
