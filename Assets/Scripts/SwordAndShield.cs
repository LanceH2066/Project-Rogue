using System;
using UnityEngine;

public class SwordAndShield : Weapons
{
    public GameManager gameManager;
    public ShieldBash shieldBash;
    private CharacterZero character;
    public SwordAndShield()
    {
        damage = 10;
        weaponName = "Sword and Shield";
    }

    public void ShieldBash(bool isPlayer)
    {
        if(isPlayer)
        {
            gameManager.enemyPositions[0].GetComponentInChildren<Enemy>().takeDamage(shieldBash.damage + damage + character.strength);
            character.hasAttacked = true;
        }
        else
        {
            gameManager.playerPositions[0].GetComponentInChildren<Knight>().takeDamage(shieldBash.damage + damage + character.strength);
        }

    }
    void Start()
    {
        character = GetComponentInParent<CharacterZero>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
