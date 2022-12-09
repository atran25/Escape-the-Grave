using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        print("here");
        if(collision.tag == "Player")
        {
            print("here2");
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
