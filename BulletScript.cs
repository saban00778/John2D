using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float Speed = 1.0f;
    private Vector2 Direction;

    public AudioClip Sound;
    private Rigidbody2D rb;
    private Animator animator;

    public int damage = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    void FixedUpdate()
    {
        rb.velocity = Direction * Speed;
        animator.SetBool("Lvl2", damage == 2);
    }

    public void toLvl2()
    {
        damage = 2;
    }

    public void setDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnMovement jhon = collision.GetComponent<JohnMovement>();
        GruntController grunt = collision.GetComponent<GruntController>();
        BossController boss = collision.GetComponent<BossController>();

        if (jhon != null) jhon.Hit();
        if (grunt != null) grunt.Hit(damage);
        if (boss != null) boss.Hit(damage);

        DestroyBullet();
    }

}
