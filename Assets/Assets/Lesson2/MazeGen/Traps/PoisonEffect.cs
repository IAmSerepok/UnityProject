using UnityEngine;
using System.Collections;

public class PoisonEffect : MonoBehaviour
{
    [Header("Настройки яда")]
    [SerializeField] private int damagePerTick = 5;
    [SerializeField] private float tickInterval = 1f;
    [SerializeField] private int totalTicks = 3;
    [SerializeField] private Color poisonColor = Color.green;
    
    private void OnTriggerEnter(Collider other)
    {
        ApplyPoison(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ApplyPoison(collision.gameObject);
    }

    private void ApplyPoison(GameObject target)
    {
        PlayerHealth health = target.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.ApplyPoison(damagePerTick, tickInterval, totalTicks, poisonColor);
        }
    }
}