using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFast : MonoBehaviour
{
    private EnemyStats enemyStats;
    private GameObject player;

    private Vector2 dir;
    private bool isInProximty = false;

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Move(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.IsTouching(GetComponent<Collider2D>()))
            if (other.CompareTag("Player"))
                other.GetComponent<PlayerStats>().TakeDmg(enemyStats.dmg);
    }

    private void Move()
    {
        if (enemyStats.isDead == true)
            return;

        if (player == null || isInProximty == true)
        {
            transform.position += (Vector3)dir * enemyStats.speed * Time.deltaTime;
            return;
        }

        if (Vector2.Distance(transform.position, player.transform.position) > 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.speed * Time.deltaTime);
            SetDir();
        }
        else
            isInProximty = true;
    }

    private void SetDir()
    {
        dir = player.transform.position - transform.position;

        dir= dir.normalized;
        transform.up = -1 * dir;
    }

}
