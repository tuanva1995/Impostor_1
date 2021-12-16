using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

    }

    public void StarEffectCoin(Vector3 startPos, Vector3 endPos, int countCoint = 10, float dur = 0.5f)
    {
        _StarEffectCoin(startPos, endPos, countCoint, dur);

    }
    void _StarEffectCoin(Vector3 startPos, Vector3 endPos, int countCoint, float dur)
    {
        List<GameObject> items = new List<GameObject>();
        for (int i = 0; i < countCoint; i++)
        {
            Vector3 newPos = startPos + new Vector3(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100), 0);
            GameObject item = GamePool.Instance.GetGameObject(GamePool.Instance.itemGold, newPos, GamePool.Instance.itemGold.transform.rotation);
            items.Add(item);
            item.transform.DOMove(endPos, dur, true).SetDelay(1f).OnComplete(() => {
                item.SetActive(false);
                Debug.Log("OnComplete =============================== " + item.name);
            });
        }
        //yield return new WaitForSeconds(0.05f);
        //for (int i=0; i< items.Count; i++)
        //{
        //    items[i].transform.DOMove(endPos, dur, true).OnComplete(() => {
        //        items[i].SetActive(false);
        //        Debug.Log("OnComplete =============================== " + items[i].name);
        //    });
        //    //yield return new WaitForSeconds(0.05f);
        //}

        Debug.Log("StarEffectCoin " + items.Count);
    }



}
