using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WIN, LOSE}

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;     // Player Character Prefabs
    public GameObject enemyPrefab;      // Enemy Character Prefabs
    public List<Transform> playerBattleStations;    // Player Battle Stations
    public List<Transform> enemyBattleStations;     // Enemy Battle Stations
    public List<Unit> playerCharacters = new List<Unit>();  // List of Player Characters In Scene
    public List<Unit> enemyCharacters = new List<Unit>();   // List of Enemy Characters In Scene
    public List<Unit> turnOrder = new List<Unit>();     // Turn Order List
    public BattleState currentState;                // Current Battle State
    private int currentTurnIndex = 0;           // Current Turn Index

    void Start()
    {
        currentState = BattleState.START;
        SetupBattle();
    }   

    private void SetupBattle()      // Spawn Units & Set Turn Order, Start Game Loop
    {
        // Spawn Units
        foreach(Transform playerBattleStation in playerBattleStations)
        {
            GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
            playerCharacters.Add(playerGO.GetComponent<Unit>());
        }
        foreach(Transform enemyBattleStation in enemyBattleStations)
        {
            GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
            enemyCharacters.Add(enemyGO.GetComponent<Unit>());
        }
        // Set Turn Order
        foreach (Unit playerCharacter in playerCharacters)
        {
            playerCharacter.initiative = Random.Range(1, 20) + playerCharacter.speed;
            turnOrder.Add(playerCharacter);
        }
        foreach (Unit enemyCharacter in enemyCharacters)
        {
            enemyCharacter.initiative = Random.Range(1, 20) + enemyCharacter.speed;
            turnOrder.Add(enemyCharacter);
        }
        turnOrder.Sort((x, y) => y.initiative.CompareTo(x.initiative));

        // Start Battle
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (currentState != BattleState.WIN && currentState != BattleState.LOSE)
        {
            Unit currentCharacter = turnOrder[currentTurnIndex];
            currentCharacter.SetCanvasVisibility(true);         // Activate Current Character UI

            if(playerCharacters.Contains(currentCharacter)) // Start Player Turn
            {
                currentState = BattleState.PLAYERTURN;
                yield return StartCoroutine(PlayerTurn());
            }
            else if(enemyCharacters.Contains(currentCharacter)) // Start Enemy Turn
            {
                currentState = BattleState.ENEMYTURN;
                yield return StartCoroutine(EnemyTurn());
            }

            currentCharacter.SetCanvasVisibility(false);    // De-activate Current Character UI
            currentTurnIndex = (currentTurnIndex + 1) % turnOrder.Count;    // Next Turn
        }
    }

    IEnumerator PlayerTurn()
    {
        Unit currentCharacter = turnOrder[currentTurnIndex];
        currentCharacter.hasAttacked = false;
        Debug.Log(currentCharacter.gameObject.name + " Turn");

        while(!currentCharacter.hasAttacked)
        {
            yield return null;
        }
    }

    IEnumerator EnemyTurn()
    {
        EnemyAttack();
        yield return new WaitForSeconds(3f);
    }

    public void PlayerAttack(Unit target)
    {
        Unit currentCharacter = turnOrder[currentTurnIndex];
        currentCharacter.Attack(target);
        currentCharacter.hasAttacked = true;
    }

    public void EnemyAttack()
    {
        int target = Random.Range(0, playerCharacters.Count);
        Unit targetCharacter = playerCharacters[target];
        Unit currentCharacter = turnOrder[currentTurnIndex];
        currentCharacter.Attack(targetCharacter);
        currentCharacter.hasAttacked = true;
    }
}
