
using UnityEngine;
using UnityEngine.InputSystem;

public class MachineGunShooter : MonoBehaviour
{
    public Shooting laserPrefab;
    public float fireRate = 0.2f;

    private float nextFireTime = 0f;

    private void Update()
    {
        if (PowerupManager.Instance == null || !PowerupManager.Instance.machineGunActive)
            return;

        if (Keyboard.current.spaceKey.isPressed && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Fire()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.LaserShot, this.transform.position);

        if (PowerupManager.Instance.doubleBulletActive)
        {
            FireDouble();
        }
        else
        {
            Shooting bullet = Instantiate(laserPrefab, this.transform.position, Quaternion.identity);
            bullet.destroyed += () => { };
        }
    }

    private void FireDouble()
    {
        Vector3 leftOffset = new Vector3(-0.5f, 0, 0);
        Vector3 rightOffset = new Vector3(0.5f, 0, 0);

        Shooting left = Instantiate(laserPrefab, transform.position + leftOffset, Quaternion.identity);
        Shooting right = Instantiate(laserPrefab, transform.position + rightOffset, Quaternion.identity);

        left.destroyed += () => { };
        right.destroyed += () => { };
    }
}
