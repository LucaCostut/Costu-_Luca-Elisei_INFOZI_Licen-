using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabBullet;
    private List<GameObject> enemyList = new List<GameObject>();

    private PlayerStats playerStats;

    private float timer;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (playerStats?.GetComponent<Rigidbody2D>().velocity != Vector2.zero) return;

        if (timer > 0)
            timer -= Time.deltaTime;
        else if (timer == 0)
        {
            GameObject target = LookForEnemy();

            if (target == null)
                return;
            timer = playerStats.reloadTime;

            Shoot(target);
        }
        else
            timer = 0;
    }

    GameObject LookForEnemy()
    {
        GameObject target = null;

        if (enemyList.Count == 0)
            return target;

        float dis = 10000;
        float aux;

        foreach (GameObject enemy in enemyList)
        {
            if (enemy.GetComponent<EnemyStats>().isDead == true)
            {
                enemyList.Remove(enemy);
                continue;
            }

            aux = Vector2.Distance(transform.position, enemy.transform.position);
            if ( aux < dis)
            {
                target = enemy;
                dis = aux;
             
            }
        }

        return target;
    }

    void Shoot(GameObject target)
    {

        playerStats.transform.up = playerStats.transform.position - target.transform.position;
        playerStats.GetComponent<Animator>().SetTrigger("Attack");

        GameObject bullet = Instantiate(prefabBullet);

        bullet.transform.position = transform.position;

        bullet.transform.up = target.transform.position - bullet.transform.position;

        bullet.GetComponent<SpriteRenderer>().material.mainTextureScale = bullet.transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            enemyList.Add(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            enemyList.Remove(other.gameObject);
    }
}
