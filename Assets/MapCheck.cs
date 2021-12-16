using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerControl player;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerControl>();
        }
        else if (Vector3.Distance(player.gameObject.transform.position, transform.position) < .3f && player.isStartTele &!player.isOutMap)
            {
            player.isOutMap = true;
            player.isStartTele = false;
            player.anim.SetFloat("moveSpeed", 0f);
            GetComponentInChildren<ParticleSystem>().Play();
            // rigid.velocity = Vector3.zero;
            player.SetTelePort(transform.position, int.Parse(gameObject.name));
        }
    }
}
