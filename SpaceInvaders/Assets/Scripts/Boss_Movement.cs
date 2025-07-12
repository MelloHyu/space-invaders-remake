using System.Diagnostics;
using UnityEngine;

public class Boss_Movements : MonoBehaviour
{
    public Boss[] prefabs;

    public int rows = 1;

    public int cols = 1;

    public float speed = 1.0f;
    public Shooting missilePrefab; 
    public float MissileAttackRate = 1.0f;

     
     
     

    private Vector3 _direction = Vector2.right;
    private void Awake()
    {
        // ✅ Move boss group (parent transform) to top center of screen
        Vector3 topCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.9f, 0f));
        topCenter.z = 0f;
        this.transform.position = topCenter;

        for (int row = 0; row < this.rows; row++)   
        {
            float width = 2.0f * (this.cols - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);

            for (int col = 0; col < this.cols; col++)
            {
                Boss boss = Instantiate(this.prefabs[row], this.transform);
                Vector3 position = rowPosition;
                position.x += col * 2.0f; // ✅ Correctly space columns
                boss.transform.localPosition = position; // ✅ Use localPosition (relative to parent)
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), this.MissileAttackRate, this.MissileAttackRate);
    }

    

    private void Update()
    {
        this.transform.position += _direction * this.speed* Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
            {
                AdvanceRow();   
            }
            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }
    private void AdvanceRow()
    {
        _direction.x *= -1.0f;
    }
    private void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Random.value < (1.0f))
            {
                Vector3 position = invader.position;

                // Fire in 3 directions: straight, left-diagonal, right-diagonal
                CreateMissile(position, Vector2.down);
                CreateMissile(position, (Vector2.down + Vector2.left).normalized);
                CreateMissile(position, (Vector2.down + Vector2.right).normalized);

                break;
            }
        }
    }        private void CreateMissile(Vector3 position, Vector2 direction)
    {
        Shooting missile = Instantiate(this.missilePrefab, position, Quaternion.identity);
        missile.direction = direction;
        missile.speed = 5f; // Adjust the speed of the boss missiles here
    }


}

