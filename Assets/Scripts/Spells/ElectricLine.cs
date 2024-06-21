using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricLine : MonoBehaviour
{
    private float dmg;

    public void SetDmg(float _dmg)
    {
        dmg = _dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            other.GetComponent<EnemyStats>().GetDmg(dmg);
    }

}
