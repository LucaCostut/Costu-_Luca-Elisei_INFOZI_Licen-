using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private static System.Random rnd = new System.Random();
    private GameObject ecran;

    private GameObject currentMapTile;
    private GameObject nextMapTile;

    [SerializeField]
    private TextMeshProUGUI textLvl;
    [SerializeField]
    private GameObject lvlUpPanel;

    [SerializeField]
    private List<GameObject> spellsListUnowned = new List<GameObject>();
    private List<GameObject> spellsListOwned   = new List<GameObject>();
    private GameObject[] lvlUpSpells = new GameObject[2];
    private int nrOfFirstSpell;

    private float hpDropAmount = 10;
    private float xpDropAmount;

    private float xpForNextLVl  = 1000;
    private float xp            = 0;
    private int lvl             = 1;

    public  float maxHp;
    private float hp;

    public float dmg;
    public float reloadTime;
    public float speed;

    void Start()
    {
        ecran = GameObject.FindGameObjectWithTag("Ecran");
        hp = maxHp;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider.CompareTag("Player"))
            if (collision.collider.CompareTag("Enemy"))
                collision.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsTouching(GetComponent<Collider2D>()))
        {
            if (other.CompareTag("HpDrop"))
            {
                TakeHp(hpDropAmount);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("XpDrop"))
            {
                TakeXp(xpDropAmount);
                Destroy(other.gameObject);
            }

            if (other.transform.parent != null)
                if (other.transform.parent.CompareTag("Map"))
                {
                    nextMapTile = other.gameObject;

                    if (currentMapTile == null)
                        currentMapTile = other.gameObject;
                }
        }



    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent != null)
            if (other.transform.parent.CompareTag("Map"))
                if (other.gameObject == currentMapTile)
                {
                    currentMapTile = nextMapTile;
                    other.transform.parent.GetComponent<Map>().SetMap(currentMapTile);
                }
    }



    public void TakeDmgOverTime(float _DMG)
    {
        hp -= _DMG * Time.deltaTime;

        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
        ecran.GetComponent<Menu>().ShowHpBar(hp, maxHp);

    }

    public void TakeDmg(float _DMG)
    {
        hp -= _DMG;

        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
        ecran.GetComponent<Menu>().ShowHpBar(hp, maxHp);

    }


    void TakeHp(float _HP)
    {
        if (hp + _HP >= maxHp)
            hp = maxHp;
        else
            hp += _HP;

        ecran.GetComponent<Menu>().ShowHpBar(hp, maxHp);

    }

    public void TakeXp(float _XP)
    {
        xp += _XP;

        if (xp >= xpForNextLVl)
        {
            xp -= xpForNextLVl;
            LvlUp();
        }
        
        ecran.GetComponent<Menu>().ShowXpBar(xp, xpForNextLVl);
    }

    void LvlUp()
    {
        Time.timeScale = 0;
        xpForNextLVl += 1000;
        xpDropAmount = xpForNextLVl / 10;
        lvl++;

        textLvl.text = "Lvl\n" + lvl.ToString();
        
        lvlUpPanel.SetActive(true);
        nrOfFirstSpell = -1;
        
        lvlUpSpells[0] = ShowLvlUpSpell(0);
        lvlUpSpells[1] = ShowLvlUpSpell(1);

        GameObject.FindObjectOfType<Map>().SetMapTexture(lvl-1);
    }

    private GameObject ShowLvlUpSpell(int spellPanelNr)
    {
        GameObject spellPanel = lvlUpPanel.transform.GetChild(spellPanelNr).gameObject;
        int spellNr;
        do
        {
            spellNr = rnd.Next(0, spellsListUnowned.Count + spellsListOwned.Count);
        } while (spellNr == nrOfFirstSpell);
        nrOfFirstSpell = spellNr;

        if(spellNr<spellsListUnowned.Count) // Show unowned spell
        {
            spellsListUnowned[spellNr].GetComponent<SpellStats>().ShowStats(spellPanel,false);
            return spellsListUnowned[spellNr];
        }
        else // Show owned spell
        {
            spellNr = spellNr - spellsListUnowned.Count;
            spellsListOwned[spellNr].GetComponent<SpellStats>().ShowStats(spellPanel,true);
            return spellsListOwned[spellNr];
        }


    }

    public void SelectLvlUpSpell(int nr)
    {
        Time.timeScale = 1;
        lvlUpPanel.SetActive(false);
        
        for(int i=0;i<spellsListOwned.Count;i++)
            if(lvlUpSpells[nr] == spellsListOwned[i])
            {
                lvlUpSpells[nr].GetComponent<SpellStats>().LvlUp();
                return;
            }
       
        // If it gets here it means the spell is not owned

        GameObject spell = Instantiate(lvlUpSpells[nr]);
        spell.transform.parent = transform.parent.GetChild(1);
        spell.transform.localPosition = Vector2.zero;
        spell.GetComponent<SpellStats>().spellName = lvlUpSpells[nr].name;

        spellsListUnowned.Remove(lvlUpSpells[nr]);
        spellsListOwned.Add(spell);
       
    }


    void Die()
    {
        ecran.GetComponent<Menu>().ReloadScene();
        GetComponent<Animator>().SetTrigger("Death");

        Destroy(transform.parent.gameObject, 1f);
    }




}
