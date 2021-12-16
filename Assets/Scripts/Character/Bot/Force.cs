using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public ParticleSystem par;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Jump();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool isPlay = false;
    public void Jump(GameObject target)
    {
         var dir =  transform.position -target.transform.position;
        dir = dir.normalized;
       // dir = new Vector3(dir.x, dir.y, dir.z);
        //rigidbody.AddForce(dir * force);
        rb.AddRelativeForce(dir * 200, ForceMode.Acceleration);
        this.enabled = false;
        if (!isPlay&&par!=null)
        {
            isPlay = true;
            par.Play();
        }
        SoundController.Instance.PlaySfx(SoundController.Instance.broken);
        rb.freezeRotation = false;
        // GetComponent<Collider>().enabled = false;
        //rb.useGravity = false;
        //SpawnCoin();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            Jump(other.gameObject);
           // Jump", .5f);
        }
    }

    [SerializeField] GameObject[] coins;
    void SpawnCoin()
    {
        foreach (GameObject g in coins) g.SetActive(true);
        //Instantiate(coin, transform.position, transform.rotation);
        GetComponent<Rigidbody>().AddExplosionForce(200, transform.position, 5);

    }
}
