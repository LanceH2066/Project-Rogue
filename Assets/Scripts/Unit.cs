using UnityEngine;

public class Unit : MonoBehaviour
{
    public int level;
    public string characterName;
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
    public Weapon weapon;
    void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
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
    public void Attack(Unit target)
    {
        if (weapon != null)
        {
            weapon.Attack(target);
        }
        else
        {
            Debug.LogWarning(characterName + " has no weapon equipped!");
        }
    }

    public void SwapWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
        Debug.Log(characterName + " swapped to " + newWeapon.weaponName);
    }
}
