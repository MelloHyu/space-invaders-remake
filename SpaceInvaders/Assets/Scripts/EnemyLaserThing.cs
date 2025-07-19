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
        if (collision.CompareTag("Player"))
        {
            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(5); // or whatever value you want
            }

            Destroy(this.gameObject); // destroy laser after hit
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
