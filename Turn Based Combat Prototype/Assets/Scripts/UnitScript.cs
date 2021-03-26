using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brackeys. "Turn-Based Combat in Unity." YouTube. November 24, 2019. [Video file] Available at: https://www.youtube.com/watch?v=_1pz_ohupPs.

public class UnitScript : MonoBehaviour
{
    public string Name;
    public string Level;

    public int Attack;
    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;
    public int MagicAttack;
      

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public bool MagicDamage(int mgc)
    {
        currentHP -= mgc;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool MP(int magic)
    {
        currentMP -= magic;

        if (currentMP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
