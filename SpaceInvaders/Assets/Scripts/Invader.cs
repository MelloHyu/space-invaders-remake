using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;

    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    public bool isBossMinion = false;
    public float shootDelay;
    public EnemyLaserThing enemyLaserThing;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }
    public void SetBossMinion(bool shootingEnemy = false, float shootDelay = 0f)
    {
        isBossMinion = true;
        this.shootDelay = shootDelay;
        if (shootingEnemy) {
            Invoke(nameof(shoot),shootDelay);
        }

    }

    public void shoot() { 
    
        Instantiate(enemyLaserThing,this.transform.position,Quaternion.identity);
        Invoke(nameof(shoot),shootDelay);
    }
    private void AnimateSprite()
    {
        _animationFrame++;

        if (_animationFrame >= this.animationSprites.Length)
        {
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Invader: OnTriggerEnter2D: {other.gameObject.name}");
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Explosion, this.transform.position);
            Destroy(other.gameObject);
            GameManager.Instance.GameOver();

        }
    }
}
