using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled;
    [SerializeField] private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField] private Transform attack1HitBoxPos;
    [SerializeField] private LayerMask whatIsDamageable;

    private bool gotInput, isAttacking, isFirstAttack;
    private float lastInputTime = Mathf.NegativeInfinity;
    private Animator anim;
    private PlayerInputAction inputActions;
    private PlayerController playerController;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        inputActions.Gameplay.Attack1.performed += context => OnAttack1(context);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
    }

    private void Update()
    {
        if (IsDialoguePlaying()) return;
        CheckAttacks();
    }

    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (IsDialoguePlaying()) return;
        if (combatEnabled)
        {
            gotInput = true;
            lastInputTime = Time.time;
        }
    }

    private void CheckAttacks()
    {
        if (gotInput )
        {
            if (!isAttacking )
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);

                CheckAttackHitBox();
            }
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.GetComponent<Health>()?.TakeDamage(attack1Damage);
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }
    private bool IsDialoguePlaying()
    {
        return DialogueManager.GetInstance().dialogueIsPlaying;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
