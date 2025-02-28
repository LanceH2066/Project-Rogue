using UnityEngine;

public class CharacterZero : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public int strength;
    public int speed;
    public float resource;
    public string resourceName;
    public int dexterity;
    public int intelligence;
    public int initiative;
    public bool hasAttacked;
    public GameObject canvas;
    public Weapons weapon;
    void Awake()
    {
        weapon = GetComponentInChildren<Weapons>();
    }
    public void SetCanvasVisibility(bool isVisible)
    {
        if (canvas != null)
        {
            canvas.SetActive(isVisible);
        }
    }
    public void displaySats()
    {
        Debug.Log("maxHP: " + maxHP);
        Debug.Log("currentHp: " + currentHP);
        Debug.Log("Strength: " + strength);
        Debug.Log("Speed: " + speed);
        Debug.Log(resourceName + ": " + resource);
        Debug.Log("Dexterity: " + dexterity);
        Debug.Log("Intelligence: " + intelligence);
        Debug.Log("Initiative: " + initiative);
    }
    public void takeDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log(gameObject.name + " Was Hit, Damage Taken: " + damage + "Remaining HP: " + currentHP);
    }

}
