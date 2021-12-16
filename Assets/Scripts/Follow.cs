using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{

    public GameObject objectToFollow;

    public float speed = 5.0f;
    public Vector3 Offset = Vector3.zero;
    void Update()
    {
        if (objectToFollow != null)
        {
            float interpolation = speed * Time.deltaTime;

            Vector3 position = this.transform.position;
            Vector3 targetPosition = objectToFollow.transform.position + Offset;
            position.y = Mathf.Lerp(this.transform.position.y, targetPosition.y, interpolation);
            position.x = Mathf.Lerp(this.transform.position.x, targetPosition.x, interpolation);

            this.transform.position = position;
        }
        else
        {
            objectToFollow = GameObject.FindGameObjectWithTag("Player");
            Offset = transform.position - objectToFollow.transform.position;
        }
    }
}