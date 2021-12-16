using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BoxPlayer")
        {
            //Debug.Log("============== COIN");
            gameObject.SetActive(false);
            UserData.Coin += 1;
            GameController.Instance.UpdatecOIN();
            SoundController.Instance.PlaySfx(SoundController.Instance.eatCoin);
            //MainCanvas.Instance.UpdateCoinUI(UserData.Coin-1, UserData.Coin);
        }

    }
}
