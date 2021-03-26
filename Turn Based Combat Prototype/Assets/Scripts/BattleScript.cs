using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Brackeys. "Turn-Based Combat in Unity." YouTube. November 24, 2019. [Video file] Available at: https://www.youtube.com/watch?v=_1pz_ohupPs. 

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleScript : MonoBehaviour
{
    public BattleState state;    

    public Text dialogue;

    public GameObject player;
    public GameObject enemy;

    public Transform mainCamera;
    public Transform enemyShadow;

    UnitScript playerUnit;
    UnitScript enemyUnit;

    public BattleHUD playerHUD;

    public KeyCode Quit;
     

    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(player, mainCamera);
        playerUnit = playerGO.GetComponent<UnitScript>();

        GameObject enemyGO = Instantiate(enemy, enemyShadow);
        enemyUnit = enemyGO.GetComponent<UnitScript>();

        dialogue.text = enemyUnit.Name + " appears!";

        playerHUD.SetHUD(playerUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        int RandNum;
        RandNum = Random.Range(0, 11);

        if (RandNum >= 9)
        {
            bool isDead = enemyUnit.TakeDamage(playerUnit.Attack * 2);

            dialogue.text = "Critical Hit! You deal" + (playerUnit.Attack * 2) + "points of damage!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else if (RandNum <= 1)
        {
            dialogue.text = "Your attack misses!";
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            bool isDead = enemyUnit.TakeDamage(playerUnit.Attack);

            dialogue.text = "You deal" + (playerUnit.Attack) + "points of damage!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        
    }

    IEnumerator PlayerHeal()
    {
        if (playerUnit.currentHP < playerUnit.maxHP)
        {
            if (playerUnit.currentMP >= 5)
            {
                playerUnit.Heal(10);
                playerUnit.MP(5);

                playerHUD.SetHP(playerUnit.currentHP);
                playerHUD.SetMP(playerUnit.currentMP);

                dialogue.text = "You heal 10 points of Health";

                yield return new WaitForSeconds(2f);

                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
            else
            {
                dialogue.text = "You dont have enough MP!";

            }
        }
        else
        {
            dialogue.text = "You are already at full HP!";
        }
        
        

    }

    IEnumerator PlayerMagic()
    {
        
        if (playerUnit.currentMP >= 8)
        {
            int MagicNumber;
            MagicNumber = Random.Range(0, 11);

            if (MagicNumber == 1)
            {
                playerUnit.MP(8);
                playerHUD.SetMP(playerUnit.currentMP);
                dialogue.text = "The spell fizzles out!";
                yield return new WaitForSeconds(2f);
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());

            }
            else
            {
                bool isDead = enemyUnit.MagicDamage(playerUnit.MagicAttack);
                playerUnit.MP(8);
                playerHUD.SetMP(playerUnit.currentMP);

                dialogue.text = "Demon King takes" + (playerUnit.MagicAttack) + "magic damage!";

                yield return new WaitForSeconds(2f);

                if (isDead)
                {
                    state = BattleState.WON;
                    EndBattle();
                }
                else
                {
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }
            }

            
        }        
        else
        {
            dialogue.text = "You dont have enough MP!";
        }

    }

    IEnumerator PlayerFlee()
    {
        int FleeChance;
        FleeChance = Random.Range(0, 4);

        if (FleeChance == 1)
        {
            dialogue.text = "The enemy prevents you from fleeing!";
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            dialogue.text = "You flee successfully!";
            yield return new WaitForSeconds(2f);
            Application.Quit();
        }
        
    }

   IEnumerator EnemyTurn()
    {
        int EnemyNumber;
        EnemyNumber = Random.Range(0, 11);

        if (EnemyNumber > 9)
        {
            dialogue.text = enemyUnit.Name + "attacks!";

            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.Attack*2);
            dialogue.text = "Adesperate attack! You take" + (enemyUnit.Attack*2) + "points of damage!";

            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {

                state = BattleState.PLAYERTURN;
                PlayerTurn();


            }
        }
        else if (EnemyNumber <= 1)
        {
            dialogue.text = enemyUnit.Name + "attacks!";
            yield return new WaitForSeconds(1f);
            dialogue.text = "You dodge the enemy attack!";
            yield return new WaitForSeconds(2f);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            dialogue.text = enemyUnit.Name + "attacks!";

            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.Attack);
            dialogue.text = "You take" + (enemyUnit.Attack) + "points of damage!";

            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {

                state = BattleState.PLAYERTURN;
                PlayerTurn();


            }
        }
        

        
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            
            dialogue.text = "You are victorious!";
            
        }
        else if (state == BattleState.LOST)
        {
            dialogue.text = "You have been slain!";
            
        }
    }

    void PlayerTurn()
    {
        dialogue.text = "Select an option!";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else if (state == BattleState.PLAYERTURN)
        {
            StartCoroutine(PlayerAttack());
        } 
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else if (state == BattleState.PLAYERTURN)
        {
            
            StartCoroutine(PlayerHeal());
        }
    }

    public void OnMagicButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else if (state == BattleState.PLAYERTURN)
        {
           
            StartCoroutine(PlayerMagic());
        }
    }

    public void OnFleeButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else if (state == BattleState.PLAYERTURN)
        {
            
            StartCoroutine(PlayerFlee());
        }
    }

    public void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }    
        
        
    }


}
