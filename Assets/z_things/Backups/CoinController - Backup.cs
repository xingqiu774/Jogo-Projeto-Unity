using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControllerBackup : MonoBehaviour
{
    
    GM_Script _GameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _GameManager = Instantiate(FindAnyObjectByType<GM_Script>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _GameManager.CreateCoin();
        Destroy(gameObject);
    }

}
