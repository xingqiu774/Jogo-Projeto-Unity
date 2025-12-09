using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_ScriptBackup : MonoBehaviour
{
    
    public GameObject Coin;
    public static GM_Script current;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateCoin()
    {
        Instantiate(Coin, new Vector2(Random.Range(Coin.transform.position.x - 5f, Coin.transform.position.x + 5f),  Coin.transform.position.y ), Quaternion.identity  );
    }


}
