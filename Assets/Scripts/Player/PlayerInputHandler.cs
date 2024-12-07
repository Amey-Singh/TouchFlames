using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour, PlayerInputAction.IGameplayActions
{
    private PlayerInputAction inputActions;
    private PlayerController playerController;
    private PlayerCombatController combatController;
    private PAttack attackController;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        playerController = GetComponent<PlayerController>();
        combatController = GetComponent<PlayerCombatController>();
        attackController = GetComponent<PAttack>();

        inputActions.Gameplay.SetCallbacks(this);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        playerController.OnMovement(context);
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        playerController.OnRoll(context);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        playerController.OnDash(context);
    }

    public void OnAttack1(InputAction.CallbackContext context)
    {
        combatController.OnAttack1(context);
    }

    public void OnAttack2(InputAction.CallbackContext context)
    {
        attackController.OnAttack2(context);
    }
}
