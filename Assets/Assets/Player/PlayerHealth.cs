using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Настройки здоровья")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float invincibilityTime = 0.5f;
    
    private int currentHealth;
    private bool isInvincible = false;
    private Coroutine poisonCoroutine;
    private Color originalMaterial;
    private Renderer playerRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        playerRenderer = GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            originalMaterial = playerRenderer.material.color;
        }
        
        Debug.Log("Инициализация здоровья игрока: " + currentHealth + "/" + maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        
        Debug.Log($"Получено урона: {damage}. Текущее здоровье: {currentHealth}");
        
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    public void ApplyPoison(int damagePerTick, float tickInterval, int totalTicks, Color poisonColor)
    {
        Debug.Log("Яд наложен на игрока!");
        
        // Останавливаем предыдущий эффект яда
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
            Debug.Log("Предыдущий эффект яда остановлен, накладывается новый");
        }
        
        poisonCoroutine = StartCoroutine(PoisonCoroutine(damagePerTick, tickInterval, totalTicks, poisonColor));
    }

    private IEnumerator PoisonCoroutine(int damagePerTick, float tickInterval, int totalTicks, Color poisonColor)
    {
        // Визуальный эффект отравления
        if (playerRenderer != null)
        {
            playerRenderer.material.color = poisonColor;
        }

        int ticksCompleted = 0;
        
        for (int i = 0; i < totalTicks; i++)
        {
            yield return new WaitForSeconds(tickInterval);
            ticksCompleted++;
            TakeDamage(damagePerTick);
            Debug.Log($"Тик яда {ticksCompleted}/{totalTicks}. Нанесено урона: {damagePerTick}");
        }

        // Возвращаем нормальный цвет
        if (playerRenderer != null)
        {
            playerRenderer.material.color = originalMaterial;
            Debug.Log("Яд закончился");
        }

        poisonCoroutine = null;
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        
        // Подсвечиваем айфреймы
        if (playerRenderer != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < invincibilityTime)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                yield return new WaitForSeconds(0.1f);
                elapsedTime += 0.1f;
            }
            playerRenderer.enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(invincibilityTime);
        }
        
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("Игрок умер!");
        // TODO логику смерти пихать сюды
    }
}