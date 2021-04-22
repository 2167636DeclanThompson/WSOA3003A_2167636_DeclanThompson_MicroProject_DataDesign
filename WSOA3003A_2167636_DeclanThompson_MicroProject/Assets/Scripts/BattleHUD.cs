using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Brackeys. "Turn-Based Combat in Unity." YouTube. November 24, 2019. [Video file] Available at: https://www.youtube.com/watch?v=_1pz_ohupPs.

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Text Health;
    public Text Magic;
    public Slider HPSlider;
    public Slider MPSlider;

    public void SetHUD(UnitScript unit)
    {
        nameText.text = unit.Name;
        levelText.text = unit.Level;
        Health.text = unit.maxHP.ToString();
        Magic.text = unit.maxMP.ToString();
        HPSlider.maxValue = unit.maxHP;
        HPSlider.value = unit.currentHP;
        MPSlider.maxValue = unit.maxMP;
        MPSlider.value = unit.currentMP;
    }

    public void SetHP(int HP)
    {
        HPSlider.value = HP;
    }

    public void SetMP(int MP)
    {
        MPSlider.value = MP;
    }
}
