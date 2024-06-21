using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyStats : MonoBehaviour
{
    private static System.Random rnd = new System.Random();


    [SerializeField]
    private GameObject prefabPopUpText;

    [SerializeField]
    private GameObject prefabHpDrop;

    [SerializeField]
    private GameObject prefabXpDrop;

    [SerializeField]
    private float xpAmount;

    [SerializeField]
    private float maxHp;
    private float hp;

    public float dmg;
    public float speed;

    [HideInInspector]
    public float canTakeAuraDmg;
    [HideInInspector]
    public bool isDead = false;

    void Start()
    {
        canTakeAuraDmg = 0;
        hp = maxHp;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider.CompareTag("Enemy"))
            if (collision.collider.CompareTag("Enemy"))
                collision.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void GetDmg(float _DMG)
    {
        hp -= _DMG;
        if (hp <= 0)
            Die();

        SetPopUpText(_DMG);
    }

    void SpawnHpDrop()
    {
        GameObject hpDrop = Instantiate(prefabHpDrop);
        hpDrop.transform.position = transform.position;
        Destroy(hpDrop, 180);
    }

    void SpawnXpDrop()
    {
        GameObject xpDrop = Instantiate(prefabXpDrop);
        xpDrop.transform.position = transform.position;
        Destroy(xpDrop, 180);

    }


    void Die()
    {
        int nr = rnd.Next(1, 101);

        if (nr <= 5)
            SpawnHpDrop();
        else if (nr <= 10)
            SpawnXpDrop();

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeXp(xpAmount);

        if (GetComponent<EnemyBomb>() != null)
            GetComponent<EnemyBomb>().StopAllCoroutines();
        isDead = true;
        GetComponent<Collider2D>().enabled = !isDead;
        GetComponent<Animator>().SetTrigger("Death");

        Destroy(this.gameObject, .5f);
    }


    void SetPopUpText(float text)
    {
        GameObject popUpText = Instantiate(prefabPopUpText);
        popUpText.transform.position = transform.position + new Vector3(Random.Range(-.2f,.2f),.25f,0);

        popUpText.GetComponent<TextMeshPro>().text = text.ToString();

        Destroy(popUpText, .5f);
    }

    public IEnumerator SetCanTakeAuraDmg()
    {
        canTakeAuraDmg = .1f;
        yield return new WaitForSeconds(canTakeAuraDmg);
        canTakeAuraDmg = 0;
    }

}
