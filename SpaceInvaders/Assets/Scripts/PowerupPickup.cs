
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    public float duration = 10f;
    public int healAmount = 15;
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
        else if (CompareTag("Powerup_Heal"))
        {
            PowerupManager.Instance.HealPlayer(other.gameObject, healAmount);
        }


        Destroy(this.gameObject);
    }
}
