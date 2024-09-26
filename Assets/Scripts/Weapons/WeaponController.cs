using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    private IWeapon weapon;
    private InputAction shootAction;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<IWeapon>();
    }

    private void OnEnable()
    {
        shootAction = new InputAction("Shoot Multiple", binding: "<mouse>/leftButton");
        shootAction.started += ShootStarted;
        shootAction.canceled += ShootCancceled;
        shootAction.Enable();
    }

    private void OnDisable()
    {
        shootAction.Disable();
    }

    private void ShootStarted(InputAction.CallbackContext context)
    {
        weapon.IsShooting = true;
    }
    private void ShootCancceled(InputAction.CallbackContext context)
    {
        weapon.IsShooting = false;
    }
}
