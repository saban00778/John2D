using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    public GameObject boss;

    void Update()
    {
        if(boss == null)
        {
            Destroy(gameObject);
        }
    }
}
