
using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;

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
}
