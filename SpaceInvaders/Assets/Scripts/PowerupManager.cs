
using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;
    public GameObject[] powerupPrefab;
    public bool machineGunActive = false;
    public bool doubleBulletActive = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ActivatePowerup(string type, float duration)
    {
        StartCoroutine(PowerupRoutine(type, duration));
    }

    private IEnumerator PowerupRoutine(string type, float duration)
    {
        if (type == "MachineGun") machineGunActive = true;
        else if (type == "DoubleBullet") doubleBulletActive = true;

        SoundManager.Instance.PlaySound(SoundManager.Sound.PowerUp);

        yield return new WaitForSeconds(duration);

        if (type == "MachineGun") machineGunActive = false;
        else if (type == "DoubleBullet") doubleBulletActive = false;
    }

    public void HealPlayer(GameObject player, int amount)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(amount);
        }
    }
    public void spawnPowerup()
    {
        if (Random.Range(0, 100) < 10) // 10% chance to spawn a powerup
        {
            GameObject powerup = Instantiate(powerupPrefab[Random.Range(0,1)]);
            powerup.transform.position = new Vector3(Random.Range(-16f, 16f), 3f, 0f);
            
        }
    }
}
