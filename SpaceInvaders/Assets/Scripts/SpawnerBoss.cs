using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpawnerBoss : MonoBehaviour, IBoss
{
    [Header("Health Settings")]
    public int maxHealth = 150;
    public int currentHealth;
    public Slider bossHealthBar;


    [Header("MiniInvader Settings")]
    public Invader[] prefabs;
    public float spawnInterval = 4f;
    private float spawnTimer;

    public Vector2 shootDelayRange;
    private List<Invader> spawnedInvaders;

    public float speed = 10;
    public Vector3 _direction;

    void Init()
    {
        currentHealth = maxHealth;
        bossHealthBar.value = 1;
        spawnTimer = spawnInterval;
        spawnedInvaders = new List<Invader>();
    }

    void Update()
    {
        HandleSpawn();
        this.transform.position += _direction * this.speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (_direction == Vector3.right && this.transform.position.x > (rightEdge.x - 1.0f))
        {
            _direction *= -1;
        }
        else if (_direction == Vector3.left && this.transform.position.x < (leftEdge.x + 1.0f))
        {
            _direction *= -1;   
        }
    }

    void HandleSpawn()
    {
        int index = Random.Range(0, prefabs.Length);
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            Vector2 randomPos = new Vector2(Random.Range(-15f, 15f), Random.Range(-4.5f, 4.5f));
            Invader invader = Instantiate(prefabs[index], randomPos, Quaternion.identity);
            spawnedInvaders.Add(invader);
            invader.SetBossMinion(true,Random.Range(shootDelayRange.x,shootDelayRange.y));
            spawnTimer = spawnInterval;
        }
    }


    public void TakeDamage(int damage)
    { 
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
        for (int i = 0; i < spawnedInvaders.Count; i++)
        {
            Destroy(spawnedInvaders[i].gameObject);
        }
        
        WaveController.Instance.BossDeath();
        spawnedInvaders = new List<Invader>();
        Destroy(gameObject);
    }

    public void SetHealthBar(Slider healthbar)
    {
        bossHealthBar = healthbar;
        Init();
    }
}
