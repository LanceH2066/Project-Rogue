using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Position> playerPositions = new List<Position>();
    public List<Position> enemyPositions = new List<Position>();
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
    private int currentTurnIndex = 0;

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

        StartCoroutine(GameLoop());
    }   

    IEnumerator GameLoop()
    {
        while (currentState != GameState.Win && currentState != GameState.Lose)
        {
            CharacterZero currentCharacter = turnOrder[currentTurnIndex];
            currentCharacter.SetCanvasVisibility(true);

            if (currentCharacter is Knight)
            {
                currentState = GameState.PlayerTurn;
                yield return StartCoroutine(PlayerTurn());
            }
            else if (currentCharacter is Enemy)
            {
                currentState = GameState.EnemyTurn;
                yield return StartCoroutine(EnemyTurn());
            }

            currentCharacter.SetCanvasVisibility(false);
            currentTurnIndex = (currentTurnIndex + 1) % turnOrder.Count;
        }
    }

    IEnumerator PlayerTurn()
    {
        CharacterZero currentCharacter = turnOrder[currentTurnIndex];
        currentCharacter.hasAttacked = false;
        Debug.Log(currentCharacter.gameObject.name + " Turn");

        while (!currentCharacter.hasAttacked)
        {
            yield return null;
        }
    }

    IEnumerator EnemyTurn()
    {
        CharacterZero currentEnemy = turnOrder[currentTurnIndex];
        currentEnemy.hasAttacked = false;
        Debug.Log(currentEnemy.gameObject.name + " Turn");

        enemyAttack();

        yield return new WaitForSeconds(3f);
    }

    public void enemyAttack()
    {
        int target = Random.Range(0, 3);
        playerPositions[target].GetComponentInChildren<CharacterZero>().takeDamage(10);
    }

}
