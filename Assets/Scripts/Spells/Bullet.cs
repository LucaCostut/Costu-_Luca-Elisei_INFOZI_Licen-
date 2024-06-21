using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 5;
    private float dmg;
    
    private void Start()
    {
        dmg = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().dmg;
        Destroy(this.gameObject, 10);
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyStats>().GetDmg(dmg);
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

}
