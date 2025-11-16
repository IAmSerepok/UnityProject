using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    private int damage;
    private float speed;
    private Vector3 direction;
    private float lifeTime = 5f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public void Initialize(int projectileDamage, float projectileSpeed, Vector3 shootDirection)
    {
        damage = projectileDamage;
        speed = projectileSpeed;
        direction = shootDirection.normalized;
        
        // Запускаем движение через Rigidbody
        rb.linearVelocity = direction * speed;
        
        // Автоматическое уничтожение через время
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, не стена ли это
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        // Наносим урон игроку
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}