using UnityEngine;

[CreateAssetMenu(fileName = "ShieldBash", menuName = "Scriptable Objects/ShieldBash")]
public class ShieldBash : ScriptableObject
{
    public float damage = 10;
    public float resourceCost = 0;
    public float attackPositions = 1;   // how many spots the attack hits
    public float attackRange = 1;   // which spots the attack can target 1-4
}
