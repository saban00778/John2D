using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{

    public GameObject BulletPrefab;
    public GameObject Respawn;
    public GameObject Canvas;
    public Image GunImg;

    private Rigidbody2D rb;
    private Animator animator;
    public AudioClip jumpSound;

    private float hor;

    public float speed = 1.0f;
    public float sprintSpeed = 1.5f;
    public float JumpForce = 1.0f;
    private float lastShoot;

    private bool Grounded;
    private bool Upgraded;

    public int health = 10;
    public int ammo = 30;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GunImg = GameObject.Find("GunIcon").GetComponent<Image>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");

        if (hor < 0.0f){ transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }
        if (hor > 0.0f) { transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }

        animator.SetBool("running", hor != 0.0f);
        animator.SetBool("jumping", !Physics2D.Raycast(transform.position, Vector3.down, 0.1f));

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else { 
            Grounded = false; 
        }

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > lastShoot + 0.35f)
        {
            Shoot();
            lastShoot = Time.time;
            animator.SetBool("standshooting", true);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();
        }

        UIScript ActUI = Canvas.GetComponent<UIScript>();

        ActUI.actUI(health, ammo);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            rb.velocity = new Vector2(hor * sprintSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(hor * speed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        if (ammo > 0) {
            Vector3 direction;

            if (transform.localScale.x == 1.0f) direction = Vector3.right;
            else direction = Vector3.left;

            GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);

            if (Upgraded) bullet.GetComponent<BulletScript>().toLvl2();

            bullet.GetComponent<BulletScript>().setDirection(direction);

            ammo--;

        }
        else
        {
            Debug.Log("No te quedan balas");
        }
    }

    public void addProps(int numHealth, int numAmmo)
    {
        health += numHealth;
        ammo += numAmmo;
    }

    public void upgradeGun()
    {
        Upgraded = true;
        GunImg.sprite = Resources.Load<Sprite>("gun_icon_lvl2");
    }

    public void Hit()
    {
        health--;

        if (health <= 0) Destroy(gameObject);
    }

    public void BossHit()
    {
        health -= 3;

        if (health <= 0) Destroy(gameObject);
    }

    public void translateJhon()
    {
        transform.position = new Vector2(Respawn.transform.position.x, Respawn.transform.position.y);

        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void EndStandShootingAnimation()
    {
        animator.SetBool("standshooting", false);
    }
}