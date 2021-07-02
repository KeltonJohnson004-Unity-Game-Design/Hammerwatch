using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackRanged : MonoBehaviour
{
    [SerializeField] public int damage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyMovement>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (other.tag != "Player" && other.tag!="Generated" && other.tag != "Projectile")
        {
            Destroy(gameObject);
        }
    }

    private float bulletLifeTime = .5f;

    private void Update()
    {
        if (bulletLifeTime > 0f)
        {
            bulletLifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
