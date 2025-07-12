using Unity.VisualScripting;
using UnityEngine;

public class EnemyLaserThing : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Explosion, this.transform.position);
            Destroy(collision.gameObject);
            GameManager.Instance.GameOver();
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
