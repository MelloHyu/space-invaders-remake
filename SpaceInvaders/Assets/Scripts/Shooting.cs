using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Vector3 direction;

    public float speed;

    public System.Action destroyed;

    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Shooting: OnTriggerEnter2D: {other.gameObject.name}");

        if (other.gameObject.CompareTag("Invader"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Explosion, this.transform.position);
            if (!other.GetComponent<Invader>().isBossMinion) {
                WaveController.Instance.InvaderDeath();
            }
            Destroy(other.gameObject);
            this.destroyed?.Invoke();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Explosion, this.transform.position);

            // Damage the boss if it has the BossController script
            IBoss boss = other.gameObject.GetComponent<IBoss>();
            if (boss != null)
            {
                boss.TakeDamage(10); // Or whatever damage value you want
            }

            this.destroyed?.Invoke();
            Destroy(this.gameObject);
        }

    }

    private void OnBecameInvisible()
    {
        this.destroyed?.Invoke();
        Destroy(this.gameObject);
    }

}