using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyJhon : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        JohnMovement jhon = collision.collider.GetComponent<JohnMovement>();

        if(jhon != null) jhon.translateJhon();
    }
}
