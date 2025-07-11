using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Shooting laserPrefab;

    public float playerSpeed = 5.0f;

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
        if(pos.x < -25.5f) pos.x = -25.5f; // Left boundary
        if(pos.x > 25.5f) pos.x = 25.5f; // Right boundary
        if(pos.y < -14f) pos.y = -14f; // Bottom boundary
        if(pos.y > 14f) pos.y = 14f; // Top boundary
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