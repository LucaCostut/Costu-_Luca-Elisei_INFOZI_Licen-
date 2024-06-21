using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellElectricCircle : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabElectricLine;

    private SpellStats stats;

    private void Start()
    {
        stats = GetComponent<SpellStats>();
        SpawnElectricLine();
        SetElectricLineLength();
    }

    void Update()
    {
            transform.eulerAngles -= new Vector3(0, 0, stats.spellRotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyStats>().GetDmg(stats.spellDmg);
    }

    void SpawnElectricLine()
    {
        GameObject electricLine = Instantiate(prefabElectricLine);
        electricLine.transform.parent = transform;
        electricLine.transform.localPosition = Vector3.zero;
        electricLine.transform.localScale = new Vector2(1, 1);
        electricLine.GetComponent<SpriteRenderer>().material.mainTextureScale = electricLine.transform.localScale;

        electricLine.GetComponent<ElectricLine>().SetDmg(stats.spellDmg);
    }

    public void SetElectricLineDmg()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetComponent<ElectricLine>().SetDmg(stats.spellDmg);      

    }

    public void SetElectricLineLength()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.localScale = new Vector2(transform.GetChild(0).transform.localScale.x, stats.spellRange);
            transform.GetChild(i).GetComponent<SpriteRenderer>().material.mainTextureScale = transform.GetChild(i).transform.localScale;
        }

    }

}
