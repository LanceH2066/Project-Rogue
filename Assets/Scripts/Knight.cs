using UnityEngine;

public class Knight : Unit
{
        public Knight()
        {
            level = 1;
            characterName = "Knight";
            maxHP = 100;
            currentHP = 100;
            strength = 10;
            speed = 5;
            resource = 0;
            resourceName = "Rage";
            dexterity = 5;
            intelligence = 3;
            hasAttacked = false;
        }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
