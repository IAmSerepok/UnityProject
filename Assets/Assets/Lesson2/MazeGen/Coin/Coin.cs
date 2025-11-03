using UnityEngine;

public class Coin : MonoBehaviour
{

    public float rotationSpeed = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, что это игрок
        if (other.CompareTag("Player"))
        {
            // Находим генератор лабиринта и сообщаем о сборе монетки
            MazeGen mazeGenerator = Object.FindFirstObjectByType<MazeGen>();
            if (mazeGenerator != null)
            {
                mazeGenerator.CollectCoin(this.gameObject);
            }
        }
    }
}
