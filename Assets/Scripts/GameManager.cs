using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        // Enable ui

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
    }   

    public void attackButtonClicked()
    {
        enemy.takeDamage(10);
    }

    public void enemyAttack()
    {
        knight.takeDamage(10);
    }
}
