using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthBarPosition : MonoBehaviour
{
    private Quaternion defRotation;
    // Start is called before the first frame update
    void Start()
    {
        defRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = defRotation;
    }
}
