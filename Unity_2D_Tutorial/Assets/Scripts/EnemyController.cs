using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    bool Vertical;
    bool Broken = true;

    Rigidbody2D rigidbody2D;
    Animator animator;

    float ChangeTime = 3.0f;
    float Change_Timer;
    int direction = 1;

    public ParticleSystem SmokeEffect;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Change_Timer = ChangeTime;
    }
    private void Update()
    {
        if (!Broken)
        {
            return;
        }
        Change_Timer -= Time.deltaTime;
        if (Change_Timer < 0)
        {
            if (Vertical)
            {
                direction = -direction;
            }
            Vertical = !Vertical;
            Change_Timer = ChangeTime;
        }
    }
    private void FixedUpdate()
    {
        if (!Broken)
        {
            return;
        }
        Vector2 Postion = rigidbody2D.position;
        if (Vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            Postion.y = Postion.y + Speed * Time.deltaTime * direction;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            Postion.x = Postion.x + Speed * Time.deltaTime * direction;
        }
        rigidbody2D.MovePosition(Postion);
    }
    public void Fix()
    {
        Broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        SmokeEffect.Stop();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController controller = collision.gameObject.GetComponent<RubyController>();
        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
