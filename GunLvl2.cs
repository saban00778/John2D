using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLvl2 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        JohnMovement john = collision.collider.GetComponent<JohnMovement>();

        if (john != null) john.upgradeGun();
        
        if (john) Destroy(gameObject);
    }
}
