using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTestLevel : MonoBehaviour
{
    public GameObject itemLevel;
    public Transform parContent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SpawnItem()
    {
        for(int i=0; i< 50; i++)
        {
            GameObject item = Instantiate(itemLevel, parContent);
            item.GetComponent<itemTestLevel>().SetItem(i + 1);
        }
    }
    private void OnEnable()
    {
        if(parContent.transform.childCount == 0)
        {
            SpawnItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void btnClose_Onclick()
    {
        gameObject.SetActive(false);
    }
}
