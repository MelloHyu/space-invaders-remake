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
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // Only horizontal movement is considered
        Vector3 moveDirection = new Vector3(movement.x, movement.y, 0);
        if (transform.position.x < -25 || transform.position.x > 25)
        {
            return;
        }
        transform.position += moveDirection * playerSpeed * Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        
    }
    private void Shoot()
    {
        if (!_laserActive) {
            Shooting projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            _laserActive = true; }
    }
    
    private void LaserDestroyed()
    {
        _laserActive = false;

    }    

}