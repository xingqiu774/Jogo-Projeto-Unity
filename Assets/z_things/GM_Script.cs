using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Script : MonoBehaviour
{
    public GameObject Coin; // Certifique-se de arrastar o prefab Coin no inspector
    public static GM_Script current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        if (Coin != null)
        {
            Instantiate(
                Coin,
                new Vector2(
                    Random.Range(Coin.transform.position.x - 5f, Coin.transform.position.x + 5f),
                    Coin.transform.position.y
                ),
                Quaternion.identity
            );
        }
        else
        {
            Debug.LogError("Prefab Coin não está definido no GameManager.");
        }
    }
}
