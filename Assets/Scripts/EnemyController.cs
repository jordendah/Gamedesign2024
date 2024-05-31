using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.0f;
    public bool vertical;
    Rigidbody2D rigidBody2D;

    public float changeTime = 3.0f;
    float Timer;
    int direction = 1;

    Animator animator;

    bool aggressive = true;

    AudioSource audio;


    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        Timer = changeTime;

        animator = GetComponent<Animator>();

        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!aggressive)
        {
            return;
        }

        Timer -= Time.deltaTime;

        if (Timer < 0)
        {
            direction = -direction;
            Timer = changeTime;
        }

        Vector2 position = rigidBody2D.position;

        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X",0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rigidBody2D.MovePosition(position);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        aggressive = false;
        rigidBody2D.simulated = false;
        animator.SetTrigger("Fixed");
        audio.Stop();
    }
}
