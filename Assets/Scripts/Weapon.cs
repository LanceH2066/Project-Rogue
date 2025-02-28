using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public Sprite icon;
    public Sprite weaponSprite;
    public void Attack(Unit target)
    {
        target.takeDamage(damage);
        Debug.Log(weaponName + " dealt " + damage + " damage to " + target.characterName);
    }
}
