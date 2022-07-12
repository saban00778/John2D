using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthAndAmmo : MonoBehaviour
{
    public int health;
    public int ammo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        JohnMovement john = collision.collider.GetComponent<JohnMovement>();

        if (john != null) john.addProps(health, ammo);

        if (john) Destroy(gameObject);
    }
}
