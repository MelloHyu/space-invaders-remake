
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    public float duration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (CompareTag("Powerup_MachineGun"))
        {
            PowerupManager.Instance.ActivatePowerup("MachineGun", duration);
        }
        else if (CompareTag("Powerup_DoubleBullet"))
        {
            PowerupManager.Instance.ActivatePowerup("DoubleBullet", duration);
        }

        Destroy(this.gameObject);
    }
}
