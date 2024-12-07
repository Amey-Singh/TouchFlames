using UnityEngine;
using UnityEngine.InputSystem;

public class PAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private float normalAttackCooldown;

    private Animator anim;
    private float normalCooldownTimer = Mathf.Infinity;
    private PlayerController playerController;
    private PlayerInputAction inputActions;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        inputActions = new PlayerInputAction();
        inputActions.Gameplay.Attack2.performed += context => OnAttack2(context);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        normalCooldownTimer += Time.deltaTime;
    }

    public void OnAttack2(InputAction.CallbackContext context)
    {
        if (IsDialoguePlaying()) return;
        if (normalCooldownTimer >= normalAttackCooldown && !playerController.IsDashing && !playerController.IsRolling && !playerController.IsCrouching && !playerController.IsWallSliding && !playerController.walking && playerController.Grounded)
        {
            NormalAttack();
        }
    }

    private void NormalAttack()
    {
        if (IsDialoguePlaying()) return;
        SoundManager.instance.PlaySound(fireballSound);
        normalCooldownTimer = 0;
        anim.SetTrigger("attack");

        int fireballIndex = FindFireball();
        if (fireballIndex != -1)
        {
            fireballs[fireballIndex].transform.position = firePoint.position;
            fireballs[fireballIndex].SetActive(true);
            fireballs[fireballIndex].GetComponent<Projectile>().SetDirection(playerController.FacingDirection);
        }
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return -1; // Indicates no available fireball
    }
    private bool IsDialoguePlaying()
    {
        return DialogueManager.GetInstance().dialogueIsPlaying;
    }
}
