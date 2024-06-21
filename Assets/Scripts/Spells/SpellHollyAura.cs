using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHollyAura : MonoBehaviour
{
    private SpellStats stats;

    private void Start()
    {
        stats = GetComponent<SpellStats>();
        SetRange();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            if (other.GetComponent<EnemyStats>().canTakeAuraDmg == 0)
            {
                StartCoroutine(other.GetComponent<EnemyStats>().SetCanTakeAuraDmg());
                other.GetComponent<EnemyStats>().GetDmg(stats.spellDmg / 10);
            }
    }

    public void SetRange()
    {
        transform.localScale = new Vector2(stats.spellRange, stats.spellRange);
    }

}
