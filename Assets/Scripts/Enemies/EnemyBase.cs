using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private EnemyStats enemyStats;
    private static GameObject player;


    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (enemyStats.isDead == true) return;
        if (player == null) return;


        Move();
        Rotate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.CompareTag("Enemy"))
            if (collision.collider.CompareTag("Player"))
                GetComponent<Animator>().SetBool("Attack", true);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherCollider.CompareTag("Enemy"))
            if (collision.collider.CompareTag("Player"))
                collision.collider.GetComponent<PlayerStats>().TakeDmgOverTime(collision.otherCollider.GetComponent<EnemyStats>().dmg);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider.CompareTag("Enemy"))
            if (collision.collider.CompareTag("Player"))
                GetComponent<Animator>().SetBool("Attack", false);
    }

    private void Move()
    {

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.speed * Time.deltaTime);
    
    }

    private void Rotate()
    {
        transform.up = transform.position - player.transform.position;
    }

}
