using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    private EnemyStats enemyStats;
    private GameObject explosion;
    private GameObject player;

    private bool isInProximty = false;

    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        explosion  = transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (enemyStats.isDead == true) return;
        if (player == null || isInProximty == true) return;

        Move();
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            if (other.IsTouching(explosion.GetComponent<CircleCollider2D>()))
                other.GetComponent<PlayerStats>().TakeDmg(GetComponent<EnemyStats>().dmg);
    }

    private void Move()
    {


        if (Vector2.Distance(transform.position, player.transform.position) < .9f)
            StartCoroutine(Explode());

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.speed * Time.deltaTime);

    }

    private void Rotate()
    {
        transform.up = transform.position - player.transform.position;
    }

    IEnumerator Explode()
    {
        isInProximty = true;
        GetComponent<Animator>().SetBool("isExploding", isInProximty);

        yield return new WaitForSeconds(1.5f);
        explosion.GetComponent<CircleCollider2D>().enabled = isInProximty;

        yield return new WaitForSeconds(.1f);
        Destroy(this.gameObject);
    }
}