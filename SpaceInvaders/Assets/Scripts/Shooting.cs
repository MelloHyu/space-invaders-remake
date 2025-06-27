using UnityEngine;

public class Shooting: MonoBehaviour
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
        if(other.gameObject.CompareTag("Invader"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Explosion, this.transform.position);
            Destroy(other.gameObject);
        }

        this.destroyed.Invoke();
        Destroy(this.gameObject);
    }
}