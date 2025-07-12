using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Shooting laserPrefab;

    public float playerSpeed = 5.0f;
    [SerializeField]
    private float leftBoundary = -25.5f;
    [SerializeField]
    private float rightBoundary = 25.5f;
    [SerializeField]
    private float topBoundary = 14f;
    [SerializeField]
    private float bottomBoundary = -14f;

    private bool _laserActive;

    private InputSystem_Actions inputActions;
    private Vector2 movement;


    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Attack.performed += Attack_performed;
        inputActions.Player.Move.canceled += OnMove;
        
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Attack.performed -= Attack_performed;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void Attack_performed(InputAction.CallbackContext obj)
    {
        if (PowerupManager.Instance != null && PowerupManager.Instance.doubleBulletActive)
            return; // double bullet shooter will handle this

        SoundManager.Instance.PlaySound(SoundManager.Sound.LaserShot, this.transform.position);
        Shoot();
    }



    private void Update()
    {
        // Only horizontal movement is considered
        Vector3 moveDirection = new Vector3(movement.x, movement.y, 0);
        Vector3 pos = transform.position + moveDirection * playerSpeed * Time.deltaTime;
        if(pos.x < leftBoundary) pos.x = leftBoundary; // Left boundary
        if(pos.x > rightBoundary) pos.x = rightBoundary; // Right boundary
        if(pos.y < bottomBoundary) pos.y = bottomBoundary; // Bottom boundary
        if(pos.y > topBoundary) pos.y = topBoundary; // Top boundary
        transform.position = pos;
    }
    private void Shoot()
    {
         
         Shooting projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
         projectile.destroyed += LaserDestroyed;
         _laserActive = true;
    }
    
    private void LaserDestroyed()
    {
        _laserActive = false;

    }    

}