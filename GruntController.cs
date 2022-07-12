using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntController : MonoBehaviour
{
    private Rigidbody2D rb;

    public GameObject John;
    public GameObject BulletPrefab;

    private float lastShoot;

    public int health = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (John == null) return;

        Vector3 direction = John.transform.position - transform.position;

        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);

        if (distance < 0.7f && Time.time > lastShoot + 0.4f)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction;

        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);

        bullet.GetComponent<BulletScript>().setDirection(direction);
    }

    public void Hit(int damage)
    {
        health -= damage;

        if (health <= 0) Destroy(gameObject);
    }
}
