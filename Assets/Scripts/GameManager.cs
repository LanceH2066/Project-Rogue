using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Knight knight;
    public Knight knight1;
    public Knight knight2;
    public Knight knight3;

    public Enemy enemy;
    public Enemy enemy1;
    public Enemy enemy2;
    public Enemy enemy3;
    public List<CharacterZero> turnOrder = new List<CharacterZero>();
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }

    public GameState currentState;

    void Start()
    {
        // Enable ui
        //new Knight[] { knight, knight1, knight2, knight3 };
        // Randomize Turn Order
        foreach (Knight knight in new Knight[] { knight, knight1, knight2, knight3 })
        {
            knight.initiative = Random.Range(1, 20) + knight.speed;
            turnOrder.Add(knight);
        }
        foreach (Enemy enemy in new Enemy[] { enemy, enemy1, enemy2, enemy3 })
        {
            enemy.initiative = Random.Range(1, 20) + enemy.speed;
            turnOrder.Add(enemy);
        }
        turnOrder.Sort((x, y) => y.initiative.CompareTo(x.initiative));
        foreach (CharacterZero character in turnOrder)
        {
            Debug.Log(character.gameObject.name + ": " + character.initiative);
        }

        StartCoroutine(GameLoop());
    }   

    IEnumerator GameLoop()
    {
        while (currentState != GameState.Win && currentState != GameState.Lose)
        {
            switch (currentState)
            {
                case GameState.PlayerTurn:
                    yield return StartCoroutine(PlayerTurn());
                    break;
                case GameState.EnemyTurn:
                    yield return StartCoroutine(EnemyTurn());
                    break;
            }
        }
    }

    IEnumerator PlayerTurn()
    {
        Debug.Log("Player Turn");

        // Wait for player input (e.g., attack button click)
        while (!Input.GetButtonDown("Fire1"))
        {
            yield return null;
        }

        // Perform player actions
        attackButtonClicked();

        // Check for win condition
        if (CheckWinCondition())
        {
            currentState = GameState.Win;
        }
        else
        {
            currentState = GameState.EnemyTurn;
        }
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy Turn");

        // Perform enemy actions
        enemyAttack();

        // Check for lose condition
        if (CheckLoseCondition())
        {
            currentState = GameState.Lose;
        }
        else
        {
            currentState = GameState.PlayerTurn;
        }

        yield return null;
    }

    public void attackButtonClicked()
    {
        enemy.takeDamage(10);
    }

    public void enemyAttack()
    {
        int target = Random.Range(1, 4);
        knight.takeDamage(10);
    }

    bool CheckWinCondition()
    {
        // Implement win condition check
        return false;
    }

    bool CheckLoseCondition()
    {
        // Implement lose condition check
        return false;
    }
}
