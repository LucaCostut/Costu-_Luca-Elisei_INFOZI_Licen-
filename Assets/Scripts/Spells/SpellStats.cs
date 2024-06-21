using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SpellStats : MonoBehaviour
{
    [HideInInspector]
    public string spellName;

    [HideInInspector]
    public int spellLvl;

    public  float spellCooldown;
    private float spellCooldownIncrease;
    public  float spellRange;
    private float spellRangeIncrease;
    public float spellRotationSpeed;
    private float spellRotationSpeedIncrease;

    public float spellDmg;
    private float spellDmgIncrease;

    private void Start()
    {
        spellLvl = 1;

        switch(spellName)
        {
            case "Holly aura":
                spellCooldownIncrease       = 0;
                spellRangeIncrease          = .1f;
                spellRotationSpeedIncrease  = 0;
                spellDmgIncrease            = 1;
                break;
            case "Zap spawner":
                spellCooldownIncrease       = .1f;
                spellRangeIncrease          = 0;
                spellRotationSpeedIncrease  = 0;
                spellDmgIncrease            = 10;
                break;
            case "Electric circle":
                spellCooldownIncrease       = 0;
                spellRangeIncrease          = .25f;
                spellRotationSpeedIncrease  = 22.5f;
                spellDmgIncrease            = 5;
                break;
        }

    }

    public void LvlUp()
    {
        spellCooldown       -= spellCooldownIncrease;
        spellDmg            += spellDmgIncrease;
        spellRotationSpeed  += spellRotationSpeedIncrease;
        spellRange          += spellRangeIncrease;



        switch (spellName)
        {
            case "Holly aura":
                GetComponent<SpellHollyAura>().SetRange();
                switch (spellLvl)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                }
                break;
            case "Zap spawner":
                switch (spellLvl)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                }
                break;
            case "Electric circle":
                GetComponent<SpellElectricCircle>().SetElectricLineLength();
                GetComponent<SpellElectricCircle>().SetElectricLineDmg();
                switch (spellLvl)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                }
                break;

        }
        spellLvl++;

    }

    public void ShowStats(GameObject spellPanel,bool isOwned)
    {

        int currentPanel = 2;
        if (isOwned == false)
        {
            
            spellPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = this.name;
            spellPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Lvl 1";

            for (int i = 2; i < 7; i++)
            {
                spellPanel.transform.GetChild(i).gameObject.SetActive(true);
                spellPanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            }

            if (spellCooldown > 0)
                spellPanel.transform.GetChild(currentPanel++).GetComponent<TextMeshProUGUI>().text = "Cooldown " + spellCooldown;
            
            if (spellRange > 0)
                spellPanel.transform.GetChild(currentPanel++).GetComponent<TextMeshProUGUI>().text = "Range " + spellRange;

            if (spellRotationSpeed > 0)
                spellPanel.transform.GetChild(currentPanel++).GetComponent<TextMeshProUGUI>().text = "Rot. speed " + GetRotationSpeedToFloatString(spellRotationSpeed);

            if (spellDmg > 0)
                spellPanel.transform.GetChild(currentPanel++).GetComponent<TextMeshProUGUI>().text = "Dmg " + spellDmg;

            for (int i = currentPanel; i < 7; i++)
                spellPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        else
        {
            
            spellPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = spellName;
            spellPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Lvl " + spellLvl + " -> " + (spellLvl + 1).ToString();

            for (int i = 2; i < 7; i++)
            {
                spellPanel.transform.GetChild(i).gameObject.SetActive(true);
                spellPanel.transform.GetChild(i).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
            }

            if (spellCooldown > 0)
                spellPanel.transform.GetChild(currentPanel++).GetComponent<TextMeshProUGUI>().text = "Cooldown " + spellCooldown + " -> " + (spellCooldown - spellCooldownIncrease).ToString();

            if (spellRange > 0)
                spellPanel.transform.GetChild(currentPanel++).GetComponent<TextMeshProUGUI>().text = "Range " + spellRange + " -> " + (spellRange + spellRangeIncrease).ToString();

            if (spellRotationSpeed > 0)
                spellPanel.transform.GetChild(currentPanel++).GetComponent<TextMeshProUGUI>().text = "Rot. speed " + GetRotationSpeedToFloatString(spellRotationSpeed) + " -> " + GetRotationSpeedToFloatString(spellRotationSpeed + spellRotationSpeedIncrease);

            if (spellDmg > 0)
                spellPanel.transform.GetChild(currentPanel++).GetComponent<TextMeshProUGUI>().text = "Dmg " + spellDmg + " -> " + (spellDmg + spellDmgIncrease).ToString();

            for (int i = currentPanel; i < 7; i++)
                spellPanel.transform.GetChild(i).gameObject.SetActive(false);

        }
    }

    string GetRotationSpeedToFloatString(float rot)
    {
        return (rot / 360).ToString("f2");
    }
}
