using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponUpgrade", menuName = "Custom/Weapon Upgrade")]
public class WeaponUpgrade : ScriptableObject
{
    public int level;
    public int damageIncrease;
    public int rangeIncrease;
    public float attackSpeedIncrease;
}
