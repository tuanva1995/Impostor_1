using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    // camera will follow this object
    public Transform Target;
    //camera transform
    public Transform camTransform;
    // offset between camera and target
    public Vector3 Offset = Vector3.zero;
    // change this value to get desired smoothness
    public float SmoothTime = .1f;

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;
    PlayerControl playerControl;
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        // update position
        if (Target != null&& Offset!=Vector3.zero)
        {
            if (playerControl.isOutMap)
                return;
            Vector3 targetPosition = Target.position + Offset;
            camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        }
        else
        {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
            playerControl = Target.GetComponent<PlayerControl>();
            Offset = camTransform.position - Target.position;
        }

        // update rotation
        //transform.LookAt(Target);
    }
}