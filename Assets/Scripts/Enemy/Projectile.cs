using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<thirdPersonMovementController>().DamagePlayer(damage);
            Destroy(gameObject);
        }
        else if(other.tag != "Enemy" && other.tag != "Projectile" && other.tag !="Generated")
        {
            Destroy(gameObject);
        }
    }
}
