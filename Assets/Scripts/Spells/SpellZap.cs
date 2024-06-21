using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellZap : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabElectricLine;

    private static System.Random rnd = new System.Random();

    private SpellStats stats;

    private int nrOfZapes = 3;

    private void Start()
    {
        stats = GetComponent<SpellStats>();
        StartCoroutine(Zap());
    }

    IEnumerator Zap()
    {
        yield return new WaitForSeconds(stats.spellCooldown);

        for (int i = 0; i < nrOfZapes; i++)
            CreateZap();

        StartCoroutine(Zap());
    }

    void CreateZap()
    {
        GameObject zap = Instantiate(prefabElectricLine);
        Vector3 rot = new Vector3(0, 0, rnd.Next(0, 360));

        zap.transform.position  = transform.position;
        zap.transform.localScale = new Vector2(1, 1);
        zap.GetComponent<SpriteRenderer>().material.mainTextureScale = zap.transform.localScale;
        zap.transform.eulerAngles = rot;
        
        zap.GetComponent<ElectricLine>().SetDmg(stats.spellDmg);
        Destroy(zap, .2f);
    }

}
