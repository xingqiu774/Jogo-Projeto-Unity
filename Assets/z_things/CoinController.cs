using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    
    GM_Script _GameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        // _GameManager = Instantiate(FindAnyObjectByType<GM_Script>());
        _GameManager = GM_Script.current;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_GameManager != null)
            {
                _GameManager.CreateCoin();
            }
        else
            {
                Debug.LogError("GameManager não encontrado.");
            }
        Destroy(gameObject);
    }

}
