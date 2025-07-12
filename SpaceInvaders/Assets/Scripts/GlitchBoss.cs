using UnityEngine;
using UnityEngine.UI;

public class GlitchBoss : MonoBehaviour, IBoss
{
    [Header("Health Settings")]
    public int maxHealth = 150;
    public int currentHealth;
    public Slider bossHealthBar;

    [Header("Glitch Flicker")]
    public float flickerInterval = 0.2f;
    private float flickerTimer;
    private bool isVulnerable = true;
    private Renderer rend;

    [Header("Teleport Settings")]
    public float teleportInterval = 4f;
    private float teleportTimer;

    [Header("Phase Management")]
    private bool phase2 = false;


    void Init()
    {
        rend = GetComponent<Renderer>();
        currentHealth = maxHealth;
        bossHealthBar.value = 1;
        flickerTimer = flickerInterval;
        teleportTimer = teleportInterval;
    }

    void Update()
    {
        HandleFlicker();
        HandleTeleport();
        HandlePhases();
    }

    void HandleFlicker()
    {
        flickerTimer -= Time.deltaTime;
        if (flickerTimer <= 0f)
        {
            isVulnerable = !isVulnerable;
            Color newColor = isVulnerable ? Color.white : new Color(1f, 1f, 1f, 0.3f);
            rend.material.color = newColor;
            flickerTimer = flickerInterval;
        }
    }

    void HandleTeleport()
    {
        teleportTimer -= Time.deltaTime;
        if (teleportTimer <= 0f)
        {
            Vector2 randomPos = new Vector2(Random.Range(-15f, 15f), Random.Range(-4.5f, 4.5f));
            transform.position = randomPos;
            teleportTimer = teleportInterval;
        }
    }

    void HandlePhases()
    {
        if (!phase2 && currentHealth <= maxHealth * 0.4f)
        {
            phase2 = true;
            teleportInterval = 1.5f;
            flickerInterval = 0.1f;
            Debug.Log("Glitch Core: Phase 2!");
        }
    }


    public void TakeDamage(int damage)
    {
        if (!isVulnerable) return;

        currentHealth -= damage;
        bossHealthBar.value = (float)currentHealth / (float)maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Glitch Core Defeated");
        bossHealthBar.gameObject.SetActive(false);
        WaveController.Instance.BossDeath();
        Destroy(gameObject);
    }

    public void SetHealthBar(Slider healthbar)
    {
        bossHealthBar = healthbar;
        Init();
    }
}
