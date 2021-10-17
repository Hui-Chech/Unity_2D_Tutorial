using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float Speed = 5.0f;
    public int MaxHealth = 5;
    public float Timeinvincible = 2.0f;

    public int Health { get { return CurrentHealth; } }
    int CurrentHealth;

    bool isInvincible;
    float Invincible_Timer;

    public GameObject ProjectilePrefab = null;
    AudioSource audioSource;


    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 LookDirection = new Vector2(1, 0);
    float Horizontal;
    float Vertical;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        CurrentHealth = MaxHealth;
    }
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        Vector2 Move = new Vector2(Horizontal, Vertical);
        if (!Mathf.Approximately(Move.x, 0) || !Mathf.Approximately(Move.y, 0))
        {
            LookDirection.Set(Move.x, Move.y);
            LookDirection.Normalize();
        }
        animator.SetFloat("Look X", LookDirection.x);
        animator.SetFloat("Look Y", LookDirection.y);
        animator.SetFloat("Speed", Move.magnitude);
        if (isInvincible)
        {
            Invincible_Timer -= Time.deltaTime;
            if (Invincible_Timer < 0)
            {
                isInvincible = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, LookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialogbox();
                }
            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 Postion = transform.position;
        Postion.x = Postion.x + Speed * Horizontal * Time.deltaTime;
        Postion.y = Postion.y + Speed * Vertical * Time.deltaTime;
        rigidbody2d.MovePosition(Postion);
    }

    public void Launch()
    {
        GameObject ProjectileObject = Instantiate(ProjectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = ProjectileObject.GetComponent<Projectile>();
        projectile.Launch(LookDirection, 300);

        animator.SetTrigger("Launch");

    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
            {
                return;
            }
            else
            {
                isInvincible = true;
                Invincible_Timer = Timeinvincible;
            }
        }
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
        UiHealthBar.instance.SetValue(CurrentHealth / (float)MaxHealth);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
