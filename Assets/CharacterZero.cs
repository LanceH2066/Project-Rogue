using UnityEngine;

public class CharacterZero : MonoBehaviour
{
    public float health;
    public float strength;
    public float speed;
    public float resource;
    public string resourceName;
    public float dexterity;
    public float intelligence;
    public float critChance;

    public CharacterZero(float health, float strength, float speed, float resource, string resourceName, float dexterity, float intelligence){
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.resource = resource;
        this.resourceName = resourceName;
        this. dexterity = dexterity;
        this.intelligence = intelligence;

    }
    public void displaySats(){
        Debug.Log("Health: " + health);
        Debug.Log("Strength: " + strength);
        Debug.Log("Speed: " + speed);
        Debug.Log(resourceName + ": " + resource);
        Debug.Log("Dexterity: " + dexterity);
        Debug.Log("Intelligence: " + intelligence);
    }
    public void takeDamage(float damage){
        health -= damage;
        if(health < 0) health = 0;
        Debug.Log("Remaining Health: " + health);
    }

    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
}
