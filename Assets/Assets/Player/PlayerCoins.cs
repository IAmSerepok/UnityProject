using UnityEngine;

public class PlayerCoins : MonoBehaviour
{

    private int coinsCollected = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinsCollected++;
            Debug.Log("Собрано монет = " + coinsCollected);

        }
    }
    
    public int GetCoinsCollected()
    {
        return coinsCollected;
    }
}
