using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum TypeItemEffectFly { coin, gem, water , key}
public class EffectItemFly : MonoBehaviour
{
    [SerializeField] Sprite spriteItemCoin;
    [SerializeField] Sprite spriteItemGem;
    [SerializeField] Sprite spriteItemKey;
    [SerializeField] GameObject prefabItemFly;


    // Start is called before the first frame update
    public static EffectItemFly Instance;

    List<int> _PooledKeyList = new List<int>();
    Dictionary<int, List<GameObject>> _PooledGoDic = new Dictionary<int, List<GameObject>>();
    void Start()
    {
        Instance = this;
    }

    public void StartEffectItemFly(Vector2 fromPos, Vector2 toPos, TypeItemEffectFly typeItem, int amount, bool isFinishGame = false, int countItem = 5)
    {
        StartCoroutine(_StartEffectItemFly(fromPos, toPos, typeItem, amount, isFinishGame, countItem));
    }

    IEnumerator _StartEffectItemFly(Vector2 fromPos, Vector2 toPos, TypeItemEffectFly typeItem, int amount, bool isFinishGame,int countItem = 5)
    {
        for (int i = 0; i < countItem; i++)
        {
            GameObject item = GetGameObject(prefabItemFly, new Vector2(fromPos.x + UnityEngine.Random.Range(-0.5f, 0.5f), fromPos.y + UnityEngine.Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            item.transform.SetParent(transform, false);
            item.transform.localScale = new Vector3(0, 0, 0);
            
            switch (typeItem)
            {
                case TypeItemEffectFly.coin:
                    //item.GetComponent<Image>().sprite = spriteItemCoin;
                    break;
                
            }
            item.transform.DOScale(2, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
           {
               item.transform.DOMove(toPos, 0.8f).SetEase(Ease.InBack).OnComplete(() => { item.SetActive(false);
                   SoundController.Instance.PlaySfx(SoundController.Instance.collect);
               });
           });
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitForSeconds(1f);
        //Set Sound
        switch (typeItem)
        {
            case TypeItemEffectFly.coin:
                SoundController.Instance.PlaySfx(SoundController.Instance.collect);

                if (!isFinishGame)
                {
                    MainCanvas.Instance.UpdateCoinUI(UserData.Coin - amount, UserData.Coin);
                }
                else
                {
                    UserData.Coin += amount;
                    GameController.Instance.UpdatecOIN();
                    Initiate.Fade("GamePlay", Color.gray, 2.0f);
                }
                break;
           
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public GameObject GetGameObject(GameObject prefab, Vector3 position, Quaternion rotation, bool forceInstantiate = false)
    {
        if (prefab == null)
        {
            return null;
        }
        int key = prefab.GetInstanceID();

        if (_PooledKeyList.Contains(key) == false && _PooledGoDic.ContainsKey(key) == false)
        {
            _PooledKeyList.Add(key);
            _PooledGoDic.Add(key, new List<GameObject>());
        }

        List<GameObject> goList = _PooledGoDic[key];
        //List<GameObject> goList = new List<GameObject>();
        GameObject go = null;

        if (forceInstantiate == false)
        {
            for (int i = goList.Count - 1; i >= 0; i--)
            {
                go = goList[i];
                if (go == null)
                {
                    goList.Remove(go);
                    continue;
                }
                if (go.activeSelf == false)
                {
                    // Found free GameObject in object pool.
                    Transform goTransform = go.transform;
                    goTransform.position = position;
                    goTransform.rotation = rotation;
                    go.SetActive(true);
                    return go;
                }
            }
        }

        // Instantiate because there is no free GameObject in object pool.
        go = (GameObject)Instantiate(prefab, position, rotation);
        go.transform.parent = transform;
        goList.Add(go);

        return go;
    }
}
