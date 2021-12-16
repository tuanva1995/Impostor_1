using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3Anim : MonoBehaviour
{
    public GameObject Torus;
    public float speed = 50f;
    public float fieldCam;
    // Start is called before the first frame update
    void Start()
    {
        if(LevelManager.GetCurrentLevelData().isActiveTorus)
            TweenControl.GetInstance().DelayCall(transform, 0.2f, () => { Torus.SetActive(true); });
        GetComponentInChildren<Camera>().fieldOfView = fieldCam;
        
    }

    // Update is called once per frame
    void Update()
    {
       // Torus.transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}
