using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
public class WaveController : MonoBehaviour
{
    public Invader[] prefabs;
    public GameObject[] boss;
    public int rows = 5;
    public int columns = 11;
    public float speed = 1.0f;
    public static int invadersDead = 0;
    private Vector3 _direction = Vector3.right;
    public Slider healthbar;


    public static WaveController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SpawnInvaders();
        }
        else
        {
            Destroy(this);  
        }
        
    }


    private void Update()
    {
        this.transform.position += _direction * this.speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (_direction == Vector3.right && invader.position.x > (rightEdge.x - 1.0f))
            {
                AdvanceRow();
            }
            else if (_direction == Vector3.left && invader.position.x < (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    public void InvaderDeath()
    {
        invadersDead++;
        if(invadersDead >= rows*columns)
        {
            BossSpawner();
            invadersDead = 0;
        }
    }

    public void BossDeath()
    {
        SpawnInvaders();
    }
    public void SpawnInvaders()
    {
        for (int row = 0; row < rows; row++)
        {
            float width = 2.0f * (columns - 1);
            float height = 2.0f * (rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);

            for (int col = 0; col < columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }

    public void BossSpawner()
    {
        int index = Random.Range(0, boss.Length);
        Vector3 position = new Vector3(0, 8, 0);
        ShowHealthBar();
        GameObject currentBoss = Instantiate(boss[index], position, Quaternion.identity);
        currentBoss.GetComponent<IBoss>().SetHealthBar(healthbar);
    }

    public void ShowHealthBar()
    {
        healthbar.gameObject.SetActive(true);
    }
}