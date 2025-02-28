using UnityEngine;
using UnityEngine.UI;

public class WeaponSwap : MonoBehaviour
{
    public Unit selectedUnit;
    public Weapon newWeapon;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (selectedUnit != null && newWeapon != null)
        {
            selectedUnit.SwapWeapon(newWeapon);
        }
    }
}