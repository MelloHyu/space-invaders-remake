using UnityEngine;
using UnityEngine.InputSystem;

public class DoubleBulletShooter : MonoBehaviour
{
    public Shooting laserPrefab;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (PowerupManager.Instance != null && PowerupManager.Instance.doubleBulletActive)
        {
            FireDouble();
        }
    }

    private void FireDouble()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.LaserShot, this.transform.position);

        Vector3 leftOffset = new Vector3(-0.5f, 0, 0);
        Vector3 rightOffset = new Vector3(0.5f, 0, 0);

        Shooting left = Instantiate(laserPrefab, transform.position + leftOffset, Quaternion.identity);
        Shooting right = Instantiate(laserPrefab, transform.position + rightOffset, Quaternion.identity);

        left.destroyed += () => { };
        right.destroyed += () => { };
    }
}
