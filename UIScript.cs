using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI vida;
    public TextMeshProUGUI municion;

    public void actUI(int health, int ammo)
    {
        vida.text = health.ToString();
        municion.text = ammo.ToString();

        if (health <= 0)
        {
            vida.text = 0.ToString();
        }
    }
}
