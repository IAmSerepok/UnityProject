using UnityEngine;


public class DamageOnContact : MonoBehaviour
{
    [Header("Настройки урона")]
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float damageCooldown = 1f; // Задержка между ударами

    private float lastDamageTime;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;

        ApplyDamage(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;

        ApplyDamage(collision.gameObject);
    }

    private void ApplyDamage(GameObject target)
    {
        // Проверяем, есть ли компонент здоровья у цели
        PlayerHealth health = target.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
            lastDamageTime = Time.time;
        }
    }
}