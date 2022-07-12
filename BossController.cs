using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject John;
    public GameObject BossBulletPrefab;
    public Slider HealthBar;

    private float lastShoot;

    public int health = 60;

    void Update()
    {
        if (John == null) return;

        Vector3 direction = John.transform.position - transform.position;

        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);

        if (distance < 4f && Time.time > lastShoot + 2.0f)
        {
            Shoot();
            lastShoot = Time.time;
        }

        HealthBar.value = health;
    }
    private void Shoot()
    {
        Vector3 direction;

        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(BossBulletPrefab, transform.position + direction * 0.155f, Quaternion.identity);

        bullet.GetComponent<BossBulletScript>().setDirection(direction);
    }

    public void Hit(int damage)
    {
        health -= damage;

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
