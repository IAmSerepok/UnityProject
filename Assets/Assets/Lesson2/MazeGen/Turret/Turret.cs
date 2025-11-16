using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    [Header("Настройки стрельбы")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float projectileSpeed = 8f;
    [SerializeField] private int damage = 5;
    
    [Header("Настройки направления")]
    [SerializeField] private Vector3 shootDirection = Vector3.forward;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    
    private float fireTimer;
    private Transform player;
    private bool playerInRange = false;
    private Vector3[] possibleDirections = {
        Vector3.forward,
        Vector3.right,
        Vector3.back,
        Vector3.left
    };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        fireTimer = fireRate;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 toPlayer = player.position - transform.position;
            float distanceToPlayer = toPlayer.magnitude;
            
            playerInRange = distanceToPlayer <= detectionRange;
            
            if (playerInRange)
            {
                // Находим ближайшее направление к игроку
                Vector3 targetDirection = GetNearestDirection(toPlayer.normalized);
                
                // Плавно поворачиваем турель
                if (shootDirection != targetDirection) {
                    shootDirection = Vector3.RotateTowards(
                        shootDirection, targetDirection, 
                        rotationSpeed * Time.deltaTime, 0f
                    ).normalized;
                    
                    if (shootDirection != Vector3.zero) {
                        transform.rotation = Quaternion.LookRotation(shootDirection);
                    }
                }

                float angle = Vector3.Angle(shootDirection, toPlayer.normalized);
                playerInRange = angle < 25f; 
            }
        }

        // Стрельба по таймеру
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f && playerInRange)
        {
            Fire();
            fireTimer = fireRate;
        }
    }

    private Vector3 GetNearestDirection(Vector3 targetDirection)
    {
        Vector3 nearestDirection = possibleDirections[0];
        float maxDot = -Mathf.Infinity;
        
        foreach (Vector3 direction in possibleDirections)
        {
            float dot = Vector3.Dot(direction, targetDirection);
            if (dot > maxDot)
            {
                maxDot = dot;
                nearestDirection = direction;
            }
        }
        
        return nearestDirection;
    }

    private void Fire()
    {
        if (projectilePrefab == null) return;

        GameObject projectile = Instantiate(
            projectilePrefab, 
            transform.position + shootDirection * 0.5f, 
            Quaternion.LookRotation(shootDirection)
        );
        
        Projectile projComponent = projectile.GetComponent<Projectile>();
        if (projComponent != null)
        {
            projComponent.Initialize(damage, projectileSpeed, shootDirection);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        shootDirection = direction.normalized;
        // Поворачиваем турель в направлении стрельбы
        if (shootDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(shootDirection);
        }
    }
}