using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 Direction;

    private Rigidbody2D rb;
    public AudioClip laserSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(laserSound);
    }

    private void FixedUpdate()
    {
        rb.velocity = Direction * speed;
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
        JohnMovement john = collision.GetComponent<JohnMovement>();
        BulletScript bullet = collision.GetComponent<BulletScript>();
        BossBulletScript bossbullet = collision.GetComponent<BossBulletScript>();

        if (john != null) john.BossHit();

        if (bullet) {}
        else if (bossbullet) {}
        else DestroyBullet(); 
    }
}
